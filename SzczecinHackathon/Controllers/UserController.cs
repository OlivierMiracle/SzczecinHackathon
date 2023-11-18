using Microsoft.AspNetCore.Mvc;
using SzczecinHackathon.DTOs;
using SzczecinHackathon.Models;
using SzczecinHackathon.Services.Interfaces;

namespace SzczecinHackathon.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IImageService _imageService;

        public UserController(IUserService userService, IImageService imageService)
        {
            _userService = userService;
            _imageService = imageService;
        }

        [HttpGet(Name = "GetUser")]
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

        [HttpPost(Name = "UploadImage")]
        public async Task<ActionResult<string>> UploadImage(ImageModel image)
        {
            var response = await _imageService.WriteImageToDisk(image.Bytes);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet(Name = "GetUserImage")]
        public async Task<ActionResult<ImageModel>> GetUserImage(string email)
        {
            var response = await _userService.GetUser(email);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
