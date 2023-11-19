using SzczecinHackathon.Models;
using SzczecinHackathon.Shared;

namespace SzczecinHackathon.Controllers
{
    public interface IMessageService
    {
        public Task<ServiceResponse<List<Message>>> GetConversationWithSelectedUser(string user, string addressee);
        public Task<ServiceResponse<List<int>>> GetUserChats(string userId);
        public Task<ServiceResponse<List<Message>>> GetChatMessages(int chatId);
        public Task<ServiceResponse<List<string>>> GetChatUsers(int chatId);
        public Task PutMessage(Message content);
        public Task PutMessageToChatWithSelectedUser(string user, string addressee, string message);
        public Task CreateChat(List<string> userIds);
        public Task<ServiceResponse<int>> CreateRandomChat(string userId);
        public Task<ServiceResponse> DeleteChat(int chat);
    }
}