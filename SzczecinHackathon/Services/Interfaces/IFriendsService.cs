using SzczecinHackathon.Shared;

namespace SzczecinHackathon.Services.Interfaces
{
    public interface IFriendsService
    {
        Task<ServiceResponse<bool>> SendInvitaion(string user, string reciver);
        Task<ServiceResponse<bool>> RejectInvitaion(string user, string sender);
        Task<ServiceResponse<bool>> AcceptInvitaion(string user, string sender);
        Task<ServiceResponse<bool>> CancelInvitaion(string user, string reciver);
        Task<ServiceResponse<bool>> RemoveFriend(string user, string personToKick);
    }
}
