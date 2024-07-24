using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NZwalks.API.Model.Domain;
using NZwalks.API.Model.DTO;

namespace NZwalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {

        [HttpGet]
        [Route("Upload")]

        public async Task<IActionResult> Upload([FromForm] UploadImageRequestDto request)
        {

            GetImageValidate(request);
            if (ModelState.IsValid) {

                var ImageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.File.FileName,
                    FileDescription = request.FileName,
                };
            
            
            
            }

            return BadRequest(ModelState);

        }

        private void GetImageValidate(UploadImageRequestDto request) {

            var validEx = new string[] { ".png", ".jpg", ".jpeg" };

            if (!validEx.Contains(Path.GetExtension(request.File.FileName))) {

                ModelState.AddModelError("File", "Format Not Supported");
            }

            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("File", "File Size Is Not Acceptable");
            }
        
        }
    }
}
