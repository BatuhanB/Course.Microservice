using Course.PhotoStock.Service.Api.Dtos;
using Course.Shared.BaseController;
using Course.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Course.PhotoStock.Service.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PhotosController : BaseController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PhotosController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> Save(IFormFile? file, CancellationToken cancellationToken)
        {
            var fileDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "photos");

            if (file == null || file.Length < 0) { return CreateActionResultInstance(Response<PhotoDto>.Fail("File is null!", 404)); }

            if (!Directory.Exists(fileDirectory))
            {
                Directory.CreateDirectory(fileDirectory);
            }

            var fileExtension = Path.GetExtension(file.FileName);
            var fileName = GetFileName(file.FileName) + fileExtension;

            var path = UniqueFilePath(fileName, fileDirectory);

            using var stream = new FileStream(path, FileMode.Create);

            await file.CopyToAsync(stream, cancellationToken);
            await stream.FlushAsync(cancellationToken);

            var result = new PhotoDto() { Url = Path.GetFileName(path) };

            return CreateActionResultInstance(Response<PhotoDto>.Success(result, 200));
        }


        [HttpDelete("{url}")]
        public IActionResult Delete(string url)
        {
            var fileDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "photos");

            var path = Path.Combine(fileDirectory, url);

            if (!System.IO.File.Exists(path)) { return CreateActionResultInstance(Response<NoContent>.Fail("File not found!", 404)); }

            System.IO.File.Delete(path);

            return CreateActionResultInstance(Response<NoContent>.Success(204));
        }

        private static string GetFileName(string fileName)
        {
            return fileName.Substring(0, fileName.LastIndexOf('.'));
        }

        private static string UniqueFilePath(string fileName, string directory)
        {
            var path = Path.Combine(directory, fileName);

            if (System.IO.File.Exists(path))
            {
                fileName = Guid.NewGuid().ToString() + fileName;
                path = Path.Combine(directory, fileName);
            }
            return path;
        }
    }
}
