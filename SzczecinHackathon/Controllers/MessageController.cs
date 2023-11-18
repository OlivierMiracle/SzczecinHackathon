﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<ServiceResponse<List<Chat>>>> GetUserChats (string userId)
        {
            var response = await _messageService.GetUserChats(userId);

            return Ok(response);
        }
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Message>>>> GetChatMessages (int chatId)
        {
            var response = await _messageService.GetChatMessages(chatId);

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
        public async Task<ActionResult> PutChat(List<string> users)
        {
            Chat chat = new Chat();
            chat.UserIds = users.ToArray();
            await _messageService.PutChat(chat);

            return Ok();
        }
    }
}