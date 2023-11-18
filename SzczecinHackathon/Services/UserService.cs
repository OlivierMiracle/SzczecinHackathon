using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SzczecinHackathon.Data;
using SzczecinHackathon.DTOs;
using SzczecinHackathon.Models;
using SzczecinHackathon.Services.Interfaces;
using SzczecinHackathon.Shared;

namespace SzczecinHackathon.Services
{
    public class UserService : IUserService
    {
        private DataContext _dataContext;

        public UserService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<CreateUserDto>> CreateUser(CreateUserDto createUserDto)
        {
            if (createUserDto == null) { return new ServiceResponse<CreateUserDto> { Success = false, Message = "Błąd 1"}; }

            _dataContext.Users.Add(Mappings.CreateUserDTO_MODEL(createUserDto));
            await _dataContext.SaveChangesAsync();

            return new ServiceResponse<CreateUserDto>
            {
                Data = createUserDto,
                Message = "Pomyślnie utworzono użytkonika",
                Success = true
            };
        }

        public async Task<ServiceResponse<GetUserDto>> GetUser(string email)
        {
            if (email == null) 
                { return new ServiceResponse<GetUserDto> { Success = false, Message = "Błąd 2" }; }

            User? user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null) 
                { return new ServiceResponse<GetUserDto> { Success = false, Message = "Nie znaleziono usera (。﹏。*)" }; }

            return new ServiceResponse<GetUserDto>
            {
                Data = Mappings.User_GetUserDTO(user),
                Message = "Jest user w bazce",
                Success = true
            };
        }

        public async Task<ServiceResponse<List<GetUserDto>>> GetUserFriendList(string email)
        {
            if (email == null)
                { return new ServiceResponse<List<GetUserDto>> { Success = false, Message = "Błąd 3" }; }

            User? user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null || user.Friends == null)
                { return new ServiceResponse<List<GetUserDto>> { Success = false, Message = "Nie znaleziono usera (。﹏。*) lub ma nulla, a nie przyjaciol" }; }

            List<GetUserDto> listToReturn = new List<GetUserDto>();

            foreach (string i in user.Friends)
            {
                User? friend = await _dataContext.Users.FirstOrDefaultAsync(x => x.Email == i);
                if (friend == null)
                    continue;

                listToReturn.Add(Mappings.User_GetUserDTO(friend));
            }

            return new ServiceResponse<List<GetUserDto>>
            {
                Data = listToReturn,
                Message = "Jest lista przyjaciol",
                Success = true
            };
        }

        public async Task<ServiceResponse<List<GetUserDto>>> GetUserFriendRequestList(string email)
        {
            if (email == null)
                { return new ServiceResponse<List<GetUserDto>> { Success = false, Message = "Błąd 5" }; }

            User? user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null || user.FriendsRequests == null)
                { return new ServiceResponse<List<GetUserDto>> { Success = false, Message = "Nie znaleziono usera (。﹏。*) lub ma nulla, a nie przyjaciol" }; }

            List<GetUserDto> listToReturn = new List<GetUserDto>();

            foreach (string i in user.FriendsRequests)
            {
                User? friend = await _dataContext.Users.FirstOrDefaultAsync(x => x.Email == i);
                if (friend == null)
                    continue;

                listToReturn.Add(Mappings.User_GetUserDTO(friend));
            }

            return new ServiceResponse<List<GetUserDto>>
            {
                Data = listToReturn,
                Message = "Jest lista przyjaciol",
                Success = true
            };
        }

        public async Task<ServiceResponse<string>> GetUserImage(string email)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null || user.ImagePath == null)
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "Podany użytkownik nie istnieje lub nie posiada zdjęcia"
                };
            }

            return new ServiceResponse<string>
            {
                Success = true,
                Message = "Sukces",
                Data = user.ImagePath
            };
        }

        public async Task<ServiceResponse<List<GetUserDto>>> GetUsers()
        {
            List<User>? users = await _dataContext.Users.ToListAsync();

            if (!users.Any())
                return new ServiceResponse<List<GetUserDto>> { Success = false, Message = "User table is empty" };

            List<GetUserDto> usersDTOs = new();

            foreach (User user in users)
            {
                usersDTOs.Add(Mappings.User_GetUserDTO(user));
            }

            return new ServiceResponse<List<GetUserDto>>
            {
                Data = usersDTOs,
                Message = "all users from db",
                Success = true
            };
        }

        public async Task<ServiceResponse<List<GetUserDto>>> GetUserSendedInvitationsList(string email)
        {
            if (email == null)
                { return new ServiceResponse<List<GetUserDto>> { Success = false, Message = "Błąd 7 I guess?" }; }

            User? user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null || user.SendedInvitations == null)
                { return new ServiceResponse<List<GetUserDto>> { Success = false, Message = "Nie znaleziono usera (。﹏。*) lub ma nulla, a nie przyjaciol" }; }

            List<GetUserDto> listToReturn = new List<GetUserDto>();

            foreach (string i in user.SendedInvitations)
            {
                User? friend = await _dataContext.Users.FirstOrDefaultAsync(x => x.Email == i);
                if (friend == null)
                    continue;

                listToReturn.Add(Mappings.User_GetUserDTO(friend));
            }

            return new ServiceResponse<List<GetUserDto>>
            {
                Data = listToReturn,
                Message = "Jest lista wyslanych zapro",
                Success = true
            };
        }
    }
}
