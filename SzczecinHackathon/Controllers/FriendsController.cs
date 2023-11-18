using Microsoft.AspNetCore.Mvc;
using SzczecinHackathon.Data;
using SzczecinHackathon.Services.Interfaces;

namespace SzczecinHackathon.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FriendsController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;

        public FriendsController(DataContext dataContext, IUserService userService)
        {
            _dataContext = dataContext;
            _userService = userService;
        }

        //[HttpGet(Name = "GetFriendList")]
        //public async Task<ActionResult<List<string>>> GetFriendList(string email)
        //{
        //    var response = await _userService.GetUser(email);

        //    if (!response.Success)
        //        return BadRequest(response);


        //}
    }
}
