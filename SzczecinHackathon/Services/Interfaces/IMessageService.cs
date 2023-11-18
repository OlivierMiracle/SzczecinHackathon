using SzczecinHackathon.Models;
using SzczecinHackathon.Shared;

namespace SzczecinHackathon.Controllers
{
    public interface IMessageService
    {
        public Task<ServiceResponse<List<Chat>>> GetUserChats(string userId);
        public Task<ServiceResponse<List<Message>>> GetChatMessages(int chatId);
        public Task PutMessage(Message content);
        public Task PutChat(Chat chat);
    }
}