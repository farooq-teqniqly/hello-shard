using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api.Models;
using api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace api.Controllers
{
    [ApiController]
    [Route("api/cows")]
    public class CowsController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public CowsController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet("users/leaders/{count}")]
        public async Task<IActionResult> GetLeaders([FromQuery] int count)
        {
            var users = await this.userRepository.GetTopUsers(count);
            var userModels = new List<LeaderModel>();

            foreach (var user in users)
            {
                userModels.Add(new LeaderModel
                {
                    Id = user.Id,
                    Clicks = user.Clicks,
                    UserName = user.Name
                });
            }

            return Ok(userModels);
        }

        [HttpPost("users/clicks")]
        public async Task<IActionResult> RecordClick(RecordClickModel model)
        {
            var user = await this.userRepository.RecordClick(model.UserId);
            var userModel = new UserModel
            {
                Id = user.Id,
                ImageUrl = user.ImageUrl,
                Name = user.Name,
                Clicks = user.Clicks
            };

            return Ok(userModel);
        }

        [HttpPost("users")]
        public async Task<IActionResult> AddUser([FromBody] AddUserModel model)
        {
            var user = await this.userRepository.AddUser(model);

            var responseModel = new UserModel
            {
                Id = user.Id,
                Name = user.Name,
                ImageUrl = user.ImageUrl
            };

            return CreatedAtRoute(
                "GetUserDetails", 
                new {userId = responseModel.Id}, 
                responseModel);
        }

        [HttpGet("users/{userId}", Name = "GetUserDetails")]
        public async Task<IActionResult> GetUserDetails(string userId)
        {
            var user = await this.userRepository.GetUserDetails(userId);

            if (user == null)
            {
                return NotFound();
            }

            var userDetailsModel = new UserDetailsModel
            {
                Id = user.Id,
                Clicks = user.Clicks,
                Name = user.Name,
                ImageUrl = user.ImageUrl
            };

            return Ok(userDetailsModel);
        }

    }
}
