using SzczecinHackathon.Shared;

namespace SzczecinHackathon.Services.Interfaces
{
    public interface IImageService
    {
        Task<ServiceResponse<string>> WriteImageToDisk(byte[] arr, string email);
    }
}
