using NZwalks.API.Model.Domain;

namespace NZwalks.API.Repositories
{
    public class LocalImageRepo : IImageRepo


    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;

        public LocalImageRepo(IWebHostEnvironment webHostEnvironment,IHttpContextAccessor httpContextAccessor)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<Image> Upload(Image image)
        {
           var LocalFIlePath=Path.Combine(webHostEnvironment.ContentRootPath,"Images",image.FileName,image.FileExtension);

            using var Streams = new FileStream(LocalFIlePath, FileMode.Create);

            await image.File.CopyToAsync(Streams);

            //to get link like -https//:localhost:12221/images/images.jpeg

            var FilePathurl = $"{httpContextAccessor.HttpContext.Request.Scheme}//{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";


        }
    }
}
