using Microsoft.AspNetCore.Mvc;
using SzczecinHackathon.DTOs;
using SzczecinHackathon.Models;
using SzczecinHackathon.Shared;

namespace SzczecinHackathon.Services.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<CreateUserDto>> CreateUser(CreateUserDto createUserDto);
        Task<ServiceResponse<GetUserDto>> GetUser(string email);
        Task<ServiceResponse<ImageModel>> GetUserImage(string email);
    }
}
