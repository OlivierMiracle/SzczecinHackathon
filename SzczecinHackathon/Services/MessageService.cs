using Microsoft.EntityFrameworkCore;
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

        public async Task<ServiceResponse<List<int>>> GetUserChats(string userId)
        {
            var data = _dataContext.Chats.Where(c => c.ChatUsers.Any(c => c.UserId == userId))
                .ToList();
            var ids = new List<int>();

            foreach (var chat in data)
            {
                ids.Add(chat.Id);
            }
            
            return new ServiceResponse<List<int>>
            {
                Data = ids,
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

        public async Task<ServiceResponse<List<string>>> GetChatUsers(int chatId)
        {
            var chatUsers = _dataContext.Chats.Include(c => c.ChatUsers).FirstOrDefault(c => c.Id == chatId).ChatUsers;
            var users = new List<string>();

            foreach (var chatUser in chatUsers)
            {
                users.Add(chatUser.UserId);
            }

            if (users.Count == 0)
            {
                return new ServiceResponse<List<string>>
                {
                    Success = false,
                    Message = "brak wiadomości"
                };
            }
            else
            {
                return new ServiceResponse<List<string>>
                {
                    Data = users,
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
                chat.ChatUsers.Add(chatUser);
            }

            _dataContext.Add(chat);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<ServiceResponse<int>> CreateRandomChat(string userId)
        {
            var response = new ServiceResponse<int>();

            var chatUsers = new List<ChatUser>();
            await _dataContext.Users.ForEachAsync(c => chatUsers.Add(new ChatUser { UserId = c.Email }));

            Chat chat = new Chat();
            _dataContext.Add(chat);

            chatUsers.ForEach(c => c.ChatId = chat.Id);
            var you = chatUsers.First(c => c.UserId == userId);
            chatUsers.Remove(you);

            Random rnd = new Random();
            int pick = rnd.Next(chatUsers.Count);

            chat.ChatUsers = new List<ChatUser> { chatUsers[pick], you };
            await _dataContext.SaveChangesAsync();
            response.Data = _dataContext.Entry(chat).Entity.Id;

            return response;
        }
        public async Task<ServiceResponse> DeleteChat(int chatId)
        {
            var toDeletion = await _dataContext.Chats.FirstOrDefaultAsync(c => c.Id == chatId);

            if (toDeletion == null)
                return new ServiceResponse { Success = false};

            _dataContext.Chats.Remove(toDeletion);
            await _dataContext.SaveChangesAsync();
             return new ServiceResponse { Success = true };
        }

        public async Task<ServiceResponse<List<Message>>> GetConversationWithSelectedUser(string user, string addressee)
        {
            var data = _dataContext.Chats.Where(c => c.ChatUsers.Any(c => c.UserId == user))
                .ToList();

            var chatsIDs = new List<int>();

            foreach (var chat in data)
            {
                chatsIDs.Add(chat.Id);
            }

            foreach (int chatId in chatsIDs)
            {
                var chatUsers = _dataContext.Chats.Include(c => c.ChatUsers).FirstOrDefault(c => c.Id == chatId).ChatUsers;
                var users = new List<string>();

                foreach (var chatUser in chatUsers)
                {
                    users.Add(chatUser.UserId);
                }

                if (users.Contains(addressee) && users.Contains(user)) 
                {
                    List<Message> messages = await _dataContext.Messages.Where(c => c.ChatId == chatId).ToListAsync();

                    return new ServiceResponse<List<Message>>
                    {
                        Data = messages,
                        Success = true,
                        Message = "tak, znaleziono czat"
                    };
                }
            }

            Chat chatNew = new Chat();

            ChatUser chatUser0 = new ChatUser { ChatId = chatNew.Id, UserId = user };
            chatNew.ChatUsers.Add(chatUser0);

            ChatUser chatUser2 = new ChatUser { ChatId = chatNew.Id, UserId = addressee };
            chatNew.ChatUsers.Add(chatUser2);

            _dataContext.Add(chatNew);
            await _dataContext.SaveChangesAsync();

            return new ServiceResponse<List<Message>>
            {
                Success = false,
                Message = "brak czatu z osoba. utworzono nowy"
            };
        }

        public async Task PutMessageToChatWithSelectedUser(string user, string addressee, string message)
        {
            if (_dataContext.Users.FirstOrDefaultAsync(c => c.Email == user) == null)
                return;

            var data = _dataContext.Chats.Where(c => c.ChatUsers.Any(c => c.UserId == user))
               .ToList();

            var chatsIDs = new List<int>();

            foreach (var chat in data)
            {
                chatsIDs.Add(chat.Id);
            }

            foreach (int chatId in chatsIDs)
            {
                var chatUsers = _dataContext.Chats.Include(c => c.ChatUsers).FirstOrDefault(c => c.Id == chatId).ChatUsers;
                var users = new List<string>();

                foreach (var chatUser in chatUsers)
                {
                    users.Add(chatUser.UserId);
                }

                if (users.Contains(addressee) && users.Contains(user))
                {
                    Message newMess = new()
                    {
                        ChatId = chatId,
                        UserId = user,
                        Content = message
                    };

                    _dataContext.Add(newMess);

                    await _dataContext.SaveChangesAsync();
                }
            }

            // When there is no chat - just do nothing
        }
    }
}
