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
        private readonly IFriendsService _friendsService;

        public FriendsController(IUserService userService, IFriendsService friendsService)
        {
            _userService = userService;
            _friendsService = friendsService;
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

        [HttpGet(Name = "GetSendedInvitationsList")]
        public async Task<ActionResult<List<GetUserDto>>> GetSendedInvitationsList(string email)
        {
            var response = await _userService.GetUserSendedInvitationsList(email);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost(Name = "SendInvitaion")]
        public async Task<ActionResult<bool>> SendInvitaion(string user, string reciver)
        {
            var response = await _friendsService.SendInvitaion(user, reciver);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost(Name = "RejectInvitaion")]
        public async Task<ActionResult<bool>> RejectInvitaion(string user, string sender)
        {
            var response = await _friendsService.RejectInvitaion(user, sender);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost(Name = "AcceptInvitaion")]
        public async Task<ActionResult<bool>> AcceptInvitaion(string user, string sender)
        {
            var response = await _friendsService.AcceptInvitaion(user, sender);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost(Name = "CancelInvitaion")]
        public async Task<ActionResult<bool>> CancelInvitaion(string user, string reciver)
        {
            var response = await _friendsService.CancelInvitaion(user, reciver);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost(Name = "RemoveFriend")]
        public async Task<ActionResult<bool>> RemoveFriend(string user, string personToKick)
        {
            var response = await _friendsService.SendInvitaion(user, personToKick);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
