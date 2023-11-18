using Microsoft.AspNetCore.Mvc;
using SzczecinHackathon.Data;
using SzczecinHackathon.DTOs;
using SzczecinHackathon.Services.Interfaces;

namespace SzczecinHackathon.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FriendsController : ControllerBase
    {
        private readonly IUserService _userService;

        public FriendsController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet(Name = "GetFriendList")]
        public async Task<ActionResult<List<GetUserDto>>> GetFriendList(string email)
        {
            var response = await _userService.GetUserFriendList(email);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet(Name = "GetFriendRequestList")]
        public async Task<ActionResult<List<GetUserDto>>> GetFriendRequestList(string email)
        {
            var response = await _userService.GetUserFriendRequestList(email);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }


    }
}
