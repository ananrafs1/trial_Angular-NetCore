using AutoMapper;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using trialApps.API.DTOs;
using trialApps.API.Helpers;
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

        // [HttpPost]
        // public async Task<IActionResult> AddPhotoForUser(int UserId, PhotoCreationDTO photoDTO) {


        // }
    }
}