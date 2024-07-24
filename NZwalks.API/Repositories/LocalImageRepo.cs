using Microsoft.AspNetCore.Http.HttpResults;
using NZwalks.API.Data;
using NZwalks.API.Model.Domain;

namespace NZwalks.API.Repositories
{
    public class LocalImageRepo : IImageRepo


    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NZWalksDBcontext dbContext;

        public LocalImageRepo(IWebHostEnvironment webHostEnvironment,IHttpContextAccessor httpContextAccessor,NZWalksDBcontext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }
        public async Task<Image> Upload(Image image)
        {
           var LocalFIlePath=Path.Combine(webHostEnvironment.ContentRootPath,"Images",$"{image.FileName}{image.FileExtension}");

            using var Streams = new FileStream(LocalFIlePath, FileMode.Create);

            await image.File.CopyToAsync(Streams);

            //to get link like -https//:localhost:12221/images/images.jpeg

            var FilePathurl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

            image.FilePath= FilePathurl;   

            await dbContext.Images.AddAsync(image);

            await dbContext.SaveChangesAsync();
            return image;


        }
    }
}
