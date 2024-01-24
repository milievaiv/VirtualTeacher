using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VirtualTeacher.Models;

namespace VirtualTeacher.Controllers
{
    public class HomeController : Controller
    {
        private readonly CloudStorageService _cloudStorageService;

        public HomeController(CloudStorageService cloudStorageService)
        {
            _cloudStorageService = cloudStorageService;
        }

        // GET: /Home/Index
        public async Task<IActionResult> Index()
        {
            var fileNames = _cloudStorageService.GetFileList("images");

            // Create a list to store image information (name and content type)
            var images = new List<ImageInfo>();

            foreach (var fileName in fileNames)
            {
                // Determine the content type based on the file extension
                string contentType = GetImageContentType(fileName);

                // Add image information to the list
                images.Add(new ImageInfo { FileName = fileName, ContentType = contentType });
            }

            return View(images);
        }

        // GET: /Home/Upload
        public IActionResult Upload()
        {
            return View("Upload");
        }

        // POST: /Home/Upload
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using (var fileStream = file.OpenReadStream())
                {
                    // Use the dynamically fetched bucket name
                    await _cloudStorageService.UploadFileAsync("images/" + file.FileName, fileStream);
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult GetImage(string imageName, string folderPath)
        {
            // Fetch the image content from Google Cloud Storage based on imageName
            byte[] imageData = _cloudStorageService.GetImageContent(imageName, folderPath);

            if (imageData != null)
            {
                // Determine the content type based on the file extension
                string contentType = GetImageContentType(imageName);

                if (contentType != null)
                {
                    return File(imageData, contentType);
                }
                else
                {
                    // Handle case where content type is not recognized
                    return BadRequest("Unsupported image format");
                }
            }
            else
            {
                // Handle case where image is not found
                return NotFound();
            }
        }

        private string GetImageContentType(string imageName)
        {
            // Determine the content type based on the file extension
            string extension = Path.GetExtension(imageName)?.ToLower();

            switch (extension)
            {
                case ".jpg":
                    return "image/jpg";
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                // Add additional cases for other supported formats
                default:
                    return null; // Unsupported format
            }
        }
    }
}