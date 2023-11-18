using Microsoft.EntityFrameworkCore;
using SzczecinHackathon.Data;
using SzczecinHackathon.Models;
using SzczecinHackathon.Services.Interfaces;
using SzczecinHackathon.Shared;

namespace SzczecinHackathon.Services
{
    public class Happening2
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<User> Participants { get; set; } = new();
    }

    public class HappeningService : IHappeningService
    {
        private readonly DataContext _dbContext;

        public HappeningService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ServiceResponse<bool>> CreateHappening(Happening happening)
        {
            var response = new ServiceResponse<bool>();

            try
            {
                _dbContext.Happenings.Add(happening);
                await _dbContext.SaveChangesAsync();
                response.Data = true;
                response.Message = "Happening created successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error creating happening: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> RemoveHappening(int happeningId)
        {
            var response = new ServiceResponse<bool>();

            try
            {
                var happening = await _dbContext.Happenings.FindAsync(happeningId);

                if (happening == null)
                {
                    response.Success = false;
                    response.Message = "Happening not found.";
                    return response;
                }

                _dbContext.Happenings.Remove(happening);
                await _dbContext.SaveChangesAsync();

                response.Data = true;
                response.Message = "Happening removed successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error removing happening: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> AttendHappening(int happeningId, string userId)
        {
            var response = new ServiceResponse<bool>();

            try
            {
                var happening = await _dbContext.Happenings
                    .Include(h => h.HappeningUsers)
                    .FirstOrDefaultAsync(h => h.Id == happeningId);

                if (happening == null)
                {
                    response.Success = false;
                    response.Message = "Happening not found.";
                    return response;
                }

                // Check if the user is already attending
                if (happening.HappeningUsers.Any(hu => hu.UserId == userId))
                {
                    response.Success = false;
                    response.Message = "User is already attending the happening.";
                    return response;
                }

                // Add the user to the HappeningUsers collection
                happening.HappeningUsers.Add(new HappeningUser { UserId = userId });
                await _dbContext.SaveChangesAsync();

                response.Data = true;
                response.Message = "User attended the happening successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error attending happening: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> UnattendHappening(int happeningId, string userId)
        {
            var response = new ServiceResponse<bool>();

            try
            {
                var happening = await _dbContext.Happenings
                    .Include(h => h.HappeningUsers)
                    .FirstOrDefaultAsync(h => h.Id == happeningId);

                if (happening == null)
                {
                    response.Success = false;
                    response.Message = "Happening not found.";
                    return response;
                }

                // Find the HappeningUser entry for the user
                var happeningUser = happening.HappeningUsers.FirstOrDefault(hu => hu.UserId == userId);

                if (happeningUser == null)
                {
                    response.Success = false;
                    response.Message = "User is not attending the happening.";
                    return response;
                }

                // Remove the user from the HappeningUsers collection
                happening.HappeningUsers.Remove(happeningUser);
                await _dbContext.SaveChangesAsync();

                response.Data = true;
                response.Message = "User unattended the happening successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error unattending happening: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> HideHappening(int happeningId, string userId)
        {
            var response = new ServiceResponse<bool>();

            try
            {
                var happening = await _dbContext.Happenings
                    .Include(h => h.HappeningUsers)
                    .FirstOrDefaultAsync(h => h.Id == happeningId);

                if (happening == null)
                {
                    response.Success = false;
                    response.Message = "Happening not found.";
                    return response;
                }

                // Find the HappeningUser entry for the user
                var happeningUser = happening.HappeningUsers.FirstOrDefault(hu => hu.UserId == userId);

                if (happeningUser == null)
                {
                    response.Success = false;
                    response.Message = "User is not attending the happening.";
                    return response;
                }

                // Perform any additional logic for hiding the happening
                happening.IsHidden = true;
                await _dbContext.SaveChangesAsync();

                response.Data = true;
                response.Message = "Happening hidden successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error hiding happening: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<Happening2>> GetHappeningWithParticipants(int happeningId)
        {
            var response = new ServiceResponse<Happening2>();

            try
            {
                var happening = await _dbContext.Happenings
                    .Include(h => h.HappeningUsers)
                    .ThenInclude(hu => hu.User) // Include the User entity for each HappeningUser
                    .FirstOrDefaultAsync(h => h.Id == happeningId);

                if (happening == null)
                {
                    response.Success = false;
                    response.Message = "Happening not found.";
                    return response;
                }

                response.Data = new Happening2
                {
                    Id = happening.Id,
                    Name = happening.Name,
                    Participants = happening.HappeningUsers?.Select(hu => hu.User).ToList()
                };

                response.Message = "Happening retrieved successfully with participants.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error getting happening with participants: {ex.Message}";
            }

            return response;
        }
    }
}
