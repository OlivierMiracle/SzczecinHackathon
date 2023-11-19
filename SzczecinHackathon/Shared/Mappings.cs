using SzczecinHackathon.DTOs;
using SzczecinHackathon.Models;

namespace SzczecinHackathon.Shared
{
    public static class Mappings
    {
        public static User CreateUserDTO_MODEL(CreateUserDto dto)
        {
            return new User
            {
                Email = dto.Email,
                Name = dto.Name,
                LastName = dto.LastName,
            };
        }

        public static GetUserDto User_GetUserDTO(User user) 
        {
            return new GetUserDto
            {
                Email = user.Email,
                Name = user.Name,
                LastName = user.LastName,
                ImagePath = user.ImagePath,
                Description = user.Description,
                Birthday = user.Birthday,
            };
        }
    }
}
