using Microsoft.AspNetCore.Mvc;
using SzczecinHackathon.DTOs;
using SzczecinHackathon.Services.Interfaces;

namespace SzczecinHackathon.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet(Name = "GetUserByEmail")]
        public async Task<ActionResult<GetUserDto>> GetUser(string email) 
        {
            var response = await _userService.GetUser(email);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost(Name = "CreateUser")]
        public async Task<ActionResult<CreateUserDto>> CreateUser(string email, string name, string lastName)
        {
            var response = await _userService.CreateUser(new CreateUserDto
            {
                Email = email,
                Name = name,
                LastName = lastName
            });

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
