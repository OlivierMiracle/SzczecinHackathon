using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SzczecinHackathon.Data;
using SzczecinHackathon.Models;
using SzczecinHackathon.Shared;

namespace SzczecinHackathon.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MessageController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IMessageService _messageService;

        public MessageController(DataContext dataContext, IMessageService messageService)
        {
            _dataContext = dataContext;
            _messageService = messageService;
        }
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<int>>>> GetUserChats (string userId)
        {
            var response = await _messageService.GetUserChats(userId);

            return Ok(response);
        }
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Message>>>> GetChatMessages (int chatId)
        {
            var response = await _messageService.GetChatMessages(chatId);

            if (response.Success == false)
                return BadRequest(response);
            else
                return Ok(response);
        }
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<string>>>> GetChatUsers(int chatId)
        {
            var response = await _messageService.GetChatUsers(chatId);

            if(response.Success == false)
                return BadRequest(response);
            else
                return Ok(response);
        }
        [HttpPut]
        public async Task<ActionResult> PutMessage(string content, int chat, string user)
        {
            await _messageService.PutMessage(
                new Message 
                {
                    Content = content, 
                    ChatId = chat, 
                    UserId = user 
                });

            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> CreateChat(List<string> userIds)
        {
            await _messageService.CreateChat(userIds);

            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<int>>> CreateRandomChat(string userId)
        {
            var response = await _messageService.CreateRandomChat(userId);

            if (response.Success == false)
                return BadRequest(response);
            else
                return Ok(response);
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteChat(int chatId)
        {
            var response = await _messageService.DeleteChat(chatId);

            if (response.Success == false)
                return BadRequest(response);
            else
                return Ok(response);
        }
    }
}
