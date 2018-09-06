using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trialApps.API.DTOs;
using trialApps.API.Repo.Interface;

namespace trialApps.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDatingRepository Repo;
        private readonly IMapper mapper;
        public UsersController(IDatingRepository Repo, IMapper mapper)
        {
            this.mapper = mapper;
            this.Repo = Repo;

        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await Repo.GetUsers();
            var ReturnedUsers = mapper.Map<IEnumerable<ListUsersDTO>>(users);
            return Ok(ReturnedUsers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await Repo.GetUser(id);
            var userToReturn = mapper.Map<DetailUserDTO>(user);

            return Ok(userToReturn);
        }
    }
}