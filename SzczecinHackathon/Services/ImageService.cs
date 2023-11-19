using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Drawing.Imaging;
using SzczecinHackathon.Data;
using SzczecinHackathon.Models;
using SzczecinHackathon.Services.Interfaces;
using SzczecinHackathon.Shared;

namespace SzczecinHackathon.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly DataContext _dataContext;

        public ImageService(IWebHostEnvironment hostingEnvironment, DataContext dataContext)
        {
            _hostingEnvironment = hostingEnvironment;
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<string>> WriteImageToDisk(byte[] arr, string email)
        {
            var filename = $@"images\{DateTime.Now.Ticks}.";
            User? user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "Gostka ni ma"
                };
            }

            //if (user.ImagePath != null) 
            //{
            //    File.Delete(user.ImagePath);
            //}

            string path = "";

            using (var im = Image.FromStream(new MemoryStream(arr)))
            {
                ImageFormat frmt;
                if (ImageFormat.Png.Equals(im.RawFormat))
                {
                    filename += "png";
                    frmt = ImageFormat.Png;
                }
                else
                {
                    filename += "jpg";
                    frmt = ImageFormat.Jpeg;
                }
                path = Path.Combine(_hostingEnvironment.ContentRootPath, filename);
                im.Save(path, frmt);
            }

            user.ImagePath = path;
            await _dataContext.SaveChangesAsync();

            return new ServiceResponse<string>
            {
                Data = $@"{path}",
                Success = true,
                Message = "Jest git"
            };
        }
    }
}
