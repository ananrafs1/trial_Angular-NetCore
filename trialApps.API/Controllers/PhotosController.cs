using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using trialApps.API.DTOs;
using trialApps.API.Helpers;
using trialApps.API.Models;
using trialApps.API.Repo.Interface;

namespace trialApps.API.Controllers
{
    [Authorize]
    [Route("api/users/{userId}/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IOptions<CloudinarySettings> cloudCongif;
        private readonly IMapper mapper;
        private readonly IDatingRepository repo;
        private Cloudinary cloudinary;
        public PhotosController(IDatingRepository repo, IMapper mapper, IOptions<CloudinarySettings> cloudCongif)
        {
            this.repo = repo;
            this.mapper = mapper;
            this.cloudCongif = cloudCongif;

            Account acc = new Account(
                this.cloudCongif.Value.CloudName,
                this.cloudCongif.Value.ApiKey,
                this.cloudCongif.Value.ApiSecret
            );

            cloudinary = new Cloudinary(acc);
        }

        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photoFromRepo = await repo.GetPhoto(id);

            var photo = mapper.Map<PhotoReturnDTO>(photoFromRepo);

            return Ok(photo);
        }


        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser(int UserId, [FromForm]PhotoCreationDTO photoDTO)
        {
            if (UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await repo.GetUser(UserId);

            var file = photoDTO.File;

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation()
                                                .Width(500)
                                                .Height(500)
                                                .Crop("fill")
                                                .Gravity("face")
                    };
                    uploadResult = cloudinary.Upload(uploadParams);
                }
            }

            photoDTO.URL = uploadResult.Uri.ToString();
            photoDTO.PublicId = uploadResult.PublicId;

            var photo = mapper.Map<Photo>(photoDTO);

            if (!userFromRepo.Photos.Any(data => data.isMain))
                photo.isMain = true;

            userFromRepo.Photos.Add(photo);

            if (await repo.SaveAll())
            {
                var ReturnedPhoto = mapper.Map<PhotoReturnDTO>(photo);
                return CreatedAtRoute("GetPhoto", new { id = photo.Id }, ReturnedPhoto);
            }

            return BadRequest("Photos failed to upload");
        }

        [HttpPost("{id}/setMain")]
        public async Task<IActionResult> SetMainPhoto(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var user = await repo.GetUser(userId);
            if (!user.Photos.Any(data => data.Id == id))
                return Unauthorized();

            var photoFromRepo = await repo.GetPhoto(id);
            if (photoFromRepo.isMain)
                return BadRequest("Already Main Photo");

            var currentMainPhoto = await repo.GetMainPhoto(userId);
            currentMainPhoto.isMain = false;

            photoFromRepo.isMain = true;

            if (await repo.SaveAll())
                return NoContent();

            return BadRequest("Failed to set Photo as Main");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var user = await repo.GetUser(userId);
            if (!user.Photos.Any(data => data.Id == id))
                return Unauthorized();

            var photoFromRepo = await repo.GetPhoto(id);
            if (photoFromRepo.isMain)
                return BadRequest("Cant Delete Main Photo");

            if (photoFromRepo.PublicId != null)
            {
                var result = cloudinary.Destroy(new DeletionParams(photoFromRepo.PublicId));

                if (result.Result == "ok")
                {
                    repo.Delete(photoFromRepo);
                }

            }
            else
            {
                repo.Delete(photoFromRepo);
            }

            if (await repo.SaveAll())
                return Ok();
                
            return BadRequest("Failed to Delete the photo");

        }
    }
}