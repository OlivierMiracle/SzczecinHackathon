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
        private readonly IWebHostEnvironment _environment;

        public UserController(IUserService userService, IImageService imageService, IWebHostEnvironment environment)
        {
            _userService = userService;
            _imageService = imageService;
            _environment = environment;
        }

        [HttpGet(Name = "GetUser")]
        public async Task<ActionResult<GetUserDto>> GetUser(string email)
        {
            var response = await _userService.GetUser(email);

            if (!response.Success)
                return Ok(response);

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
                return Ok(response);

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
        public IActionResult GetUserImage(string imageName)
        {
            var imgPath = Path.Combine(_environment.ContentRootPath, @"images\" + imageName);

            if (System.IO.File.Exists(imgPath))
            {
                var imageBytes = System.IO.File.ReadAllBytes(imgPath);
                return File(imageBytes, "image/jpeg"); // Adjust the content type based on your image type
            }

            return BadRequest();
        }
    }
}
