﻿using Microsoft.AspNetCore.Mvc;
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

        public Task<ServiceResponse<ImageModel>> GetUserImage(string email)
        {
            throw new NotImplementedException();
        }
    }
}