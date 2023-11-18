using Microsoft.EntityFrameworkCore;
using System.Linq;
using SzczecinHackathon.Controllers;
using SzczecinHackathon.Data;
using SzczecinHackathon.Models;
using SzczecinHackathon.Shared;

namespace SzczecinHackathon.Services
{
    public class MessageService : IMessageService
    {
        private readonly DataContext _dataContext;
        public MessageService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<ServiceResponse<List<Chat>>> GetUserChats(string userId)
        {
            var data = _dataContext.Chats.Where(c => c.ChatUsers.Any(c => c.UserId == userId))
                .Include(c => c.ChatUsers)
                .ToList();
            
            return new ServiceResponse<List<Chat>>
            {
                Data = data,
                Success = true
            };
        }
        public async Task<ServiceResponse<List<Message>>> GetChatMessages(int chatId)
        {
            var messages = _dataContext.Messages.Where(c => c.ChatId == chatId).ToList();

            if (messages.Count == 0)
            {
                return new ServiceResponse<List<Message>>
                {
                    Success = false,
                    Message = "brak wiadomości"
                };
            }
            else
            {
                return new ServiceResponse<List<Message>>
                {
                    Data = messages,
                    Success = true
                };
            }
        }
        public async Task PutMessage(Message message)
        {
            if (_dataContext.Users.FirstOrDefaultAsync(c => c.Email == message.UserId) == null)
                return;

            if (message.Id == default)
                _dataContext.Add(message);
            else
                _dataContext.Update(message);

            await _dataContext.SaveChangesAsync();
        }
        public async Task CreateChat(List<string> users)
        {
            Chat chat = new Chat();
            var theoretical = _dataContext.Users.Where(user => users.Contains(user.Email)).ToList();


            if (theoretical.Count == 0)
                return;

            foreach(var item in theoretical)
            {
                ChatUser chatUser = new ChatUser { ChatId = chat.Id, UserId = item.Email};
                _dataContext.ChatUsers.Add(chatUser);
                chat.ChatUsers.Add(chatUser);
            }

            _dataContext.Add(chat);
            await _dataContext.SaveChangesAsync();
        }
    }
}
