using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using trialApps.API.DTOs;
using trialApps.API.Models;
using trialApps.API.Repo.Interface;

namespace trialApps.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository repo;
        private readonly IConfiguration config;
        private readonly IMapper mapper;

        public AuthController(IAuthRepository repo, IConfiguration config, IMapper mapper)
        {
            this.mapper = mapper;
            this.config = config;
            this.repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> register(UserForRegisterDTO userDTO)
        {
            userDTO.Username = userDTO.Username.ToLower();

            if (await repo.UserExists(userDTO.Username))
            {
                return BadRequest("Username already exists");
            }

            User userToCreate = mapper.Map<User>(userDTO);

            var createdUser = await repo.Register(userToCreate, userDTO.Password);

            var userToReturn = mapper.Map<DetailUserDTO>(createdUser);

            return CreatedAtRoute("GetUser",new {controller="Users", id=createdUser.Id}, userToReturn);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDTO UserDTO)
        {
            //check account in db
            var userFromRepo = await repo.Login(UserDTO.Username.ToLower(), UserDTO.Password);

            // validate if user not found
            if (userFromRepo == null)
            {
                return Unauthorized();
            }

            // create "claims" for claimsId in token Descriptor
            var claims = new[]{
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };
            // create "Credential" for signing Credential in token Descriptor
            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // create token Descriptor Object for JWT
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var user = mapper.Map<ListUsersDTO>(userFromRepo);
            // return JWT token
            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                user
            });
        }
    }
}