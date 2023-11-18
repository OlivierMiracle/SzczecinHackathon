using SzczecinHackathon.DTOs;
using SzczecinHackathon.Shared;

namespace SzczecinHackathon.Services.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<CreateUserDto>> CreateUser(CreateUserDto createUserDto);
        Task<ServiceResponse<GetUserDto>> GetUser(string email);
        Task<ServiceResponse<List<GetUserDto>>> GetUsers();
        Task<ServiceResponse<List<GetUserDto>>> GetUserFriendList(string email);
        Task<ServiceResponse<List<GetUserDto>>> GetUserFriendRequestList(string email);
        Task<ServiceResponse<List<GetUserDto>>> GetUserSendedInvitationsList(string email);
        Task<ServiceResponse<string>> GetUserImage(string email);
    }
}
