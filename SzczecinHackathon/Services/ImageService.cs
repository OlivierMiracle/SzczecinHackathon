using System.Drawing;
using System.Drawing.Imaging;
using SzczecinHackathon.Services.Interfaces;
using SzczecinHackathon.Shared;

namespace SzczecinHackathon.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ImageService(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<ServiceResponse<string>> WriteImageToDisk(byte[] arr)
        {
            var filename = $@"images\{DateTime.Now.Ticks}.";

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

            return new ServiceResponse<string>
            {
                Data = $@"http:\\{path}",
                Success = true,
                Message = "Jest git"
            };
        }
    }
}
