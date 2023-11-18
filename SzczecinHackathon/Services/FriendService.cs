using Microsoft.EntityFrameworkCore;
using System.Reflection;
using SzczecinHackathon.Data;
using SzczecinHackathon.Models;
using SzczecinHackathon.Services.Interfaces;
using SzczecinHackathon.Shared;

namespace SzczecinHackathon.Services
{
    public class FriendService : IFriendsService
    {
        private readonly DataContext _dataContext;

        public FriendService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<bool>> SendInvitaion(string user, string reciver)
        {
            User? userModel = await GetUserFromDB(user);
            User? reciverModel = await GetUserFromDB(reciver);

            if (reciverModel == null || userModel == null)
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "User do ktorego inv dotn istnieje lub nie istniejesz ty. hyhyhy"
                };

            userModel.SendedInvitations = userModel.SendedInvitations.Append(reciverModel.Email).ToArray();
            reciverModel.FriendsRequests = reciverModel.FriendsRequests.Append(userModel.Email).ToArray();

            await _dataContext.SaveChangesAsync();

            return new ServiceResponse<bool>
            {
                Success = true,
                Message = "Wysylanie zapro git",
                Data = true
            };
        }

        public async Task<ServiceResponse<bool>> AcceptInvitaion(string user, string sender)
        {
            User? userModel = await GetUserFromDB(user);
            User? senderModel = await GetUserFromDB(sender);

            if (senderModel == null || userModel == null)
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "User od ktorego inv akceptujesz nie istnieje lub nie istniejesz ty. hyhyhy"
                };

            userModel.FriendsRequests = userModel.FriendsRequests.Where(val => val != senderModel.Email).ToArray();
            userModel.Friends = userModel.Friends.Append(senderModel.Email).ToArray();

            senderModel.SendedInvitations = senderModel.SendedInvitations.Where(val => val != userModel.Email).ToArray();
            senderModel.Friends = senderModel.Friends.Append(userModel.Email).ToArray();

            await _dataContext.SaveChangesAsync();

            return new ServiceResponse<bool>
            {
                Success = true,
                Message = "Przyjecie zapro git",
                Data = true
            };
        }

        public async Task<ServiceResponse<bool>> RejectInvitaion(string user, string sender)
        {
            User? userModel = await GetUserFromDB(user);
            User? senderModel = await GetUserFromDB(sender);

            if (senderModel == null || userModel == null)
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "User od ktorego inv wypierdalasz nie istnieje lub nie istniejesz ty. hyhyhy"
                };

            userModel.FriendsRequests = userModel.FriendsRequests.Where(val => val != senderModel.Email).ToArray();

            senderModel.SendedInvitations = senderModel.SendedInvitations.Where(val => val != userModel.Email).ToArray();

            await _dataContext.SaveChangesAsync();

            return new ServiceResponse<bool>
            {
                Success = true,
                Message = "Odrzucenie zapro git",
                Data = true
            };
        }

        public async Task<ServiceResponse<bool>> CancelInvitaion(string user, string reciver)
        {
            User? userModel = await GetUserFromDB(user);
            User? reciverModel = await GetUserFromDB(reciver);

            if (reciverModel == null || userModel == null)
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "User do ktorego chesz cancelować zapro nie istnieje lub nie istniejesz ty. hyhyhy"
                };

            userModel.SendedInvitations = userModel.SendedInvitations.Where(x => x != reciverModel.Email).ToArray();
            reciverModel.FriendsRequests = reciverModel.FriendsRequests.Where(x => x != userModel.Email).ToArray();

            await _dataContext.SaveChangesAsync();

            return new ServiceResponse<bool>
            {
                Success = true,
                Message = "Cancelowanie zapro git",
                Data = true
            };
        }

        public async Task<ServiceResponse<bool>> RemoveFriend(string user, string personToKick)
        {
            User? userModel = await GetUserFromDB(user);
            User? personToKickModel = await GetUserFromDB(personToKick);

            if (personToKickModel == null || userModel == null)
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "User ktorego chesz wyjebać nie istnieje lub nie istniejesz ty. hyhyhy"
                };

            userModel.Friends = userModel.Friends.Where(x => x != personToKickModel.Email).ToArray();
            personToKickModel.Friends = personToKickModel.Friends.Where(x => x != userModel.Email).ToArray();

            await _dataContext.SaveChangesAsync();

            return new ServiceResponse<bool>
            {
                Success = true,
                Message = "Wyjebanie zapro git",
                Data = true
            };
        }

        private async Task<User?> GetUserFromDB(string email) 
        {
            return await _dataContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
