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
            var data = _dataContext.Chats.Where(chat => chat.UserIds.ToList().Any(x => x == userId)).ToList();

            return new ServiceResponse<List<Chat>>
            {
                Data = data,
                Success = true
            };
        }
        public async Task<ServiceResponse<List<Message>>> GetChatMessages(int chatId)
        {
            var chat = _dataContext.Chats.FirstOrDefault(chat => chat.Id == chatId);

            if (chat == null)
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
                    Data = chat.Messages,
                    Success = true
                };
            }
        }
        public async Task PutMessage(Message message)
        {
            if (message.Id == default)
                _dataContext.Add(message);
            else
                _dataContext.Update(message);

            await _dataContext.SaveChangesAsync();
        }
        public async Task PutChat(Chat chat)
        {
            if (chat.Id == default)
                _dataContext.Add(chat);
            else
                _dataContext.Update(chat);

            await _dataContext.SaveChangesAsync();
        }
    }
}
