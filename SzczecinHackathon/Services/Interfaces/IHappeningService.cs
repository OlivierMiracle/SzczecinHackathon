using SzczecinHackathon.Models;
using SzczecinHackathon.Shared;

namespace SzczecinHackathon.Services.Interfaces
{
    public interface IHappeningService
    {
        Task<ServiceResponse<bool>> CreateHappening(Happening happening);
        Task<ServiceResponse<bool>> RemoveHappening(int happeningId);
        Task<ServiceResponse<bool>> AttendHappening(int happeningId, string userId);
        Task<ServiceResponse<bool>> UnattendHappening(int happeningId, string userId);
        Task<ServiceResponse<bool>> HideHappening(int happeningId, string userId);
        Task<ServiceResponse<Happening2>> GetHappeningWithParticipants(int happeningId);
    }
}
