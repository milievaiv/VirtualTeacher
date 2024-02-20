using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using System.Security.Claims;
using VirtualTeacher.Attributes;
using VirtualTeacher.Services;
using VirtualTeacher.Services.Contracts;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace VirtualTeacher.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly CloudStorageService _cloudStorageService;
        private readonly IStudentService studentService;
        private readonly IAssignmentService assignmentService;

        public AssignmentController(IWebHostEnvironment hostingEnvironment, CloudStorageService cloudStorageService, IAssignmentService assignmentService, IStudentService studentService)
        {
            _hostingEnvironment = hostingEnvironment;
            _cloudStorageService = cloudStorageService;
            this.studentService = studentService;
            this.assignmentService = assignmentService;
        }


        //Possible cookie implementation to store a token of some kind containing the assignment id for safety measures
        [AuthorizeUsers("student")]
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file, int assignmentId)
        {
            var user = HttpContext.User;
            var email = user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
            var student = studentService.GetByEmail(email);
            var assignment = assignmentService.GetById(assignmentId);
            var lecture = assignment.Lecture;
            var course = lecture.Course;

            if (!assignmentService.IsAssignmentSubmitted(student, assignment))
            {
                if (file != null && file.Length > 0)
                {
                    using (var fileStream = file.OpenReadStream())
                    {
                        var ext = Path.GetExtension(file.FileName);
                        string newFileName = student.Id + ext;
                        string filePath = $"Courses/{course.Id}/Lectures/{lecture.Id}/Assignments/{assignmentId}/";
                        await _cloudStorageService.UploadFileAsync(filePath + newFileName, fileStream);
                    }
                }
                assignmentService.SubmitAssignment(student, assignment);

                return Json(new { success = true });

            }
            return Json(new { success = false });
        }

        [HttpGet]
        public IActionResult GetContentFile([FromQuery] string folderPath)
        {
            string fileName = Path.GetFileName(folderPath);
            byte[] fileData = _cloudStorageService.GetFileContent(folderPath);
            //string fileName = "assignment-content1.pdf";
            //folderPath = _hostingEnvironment.WebRootPath;

            //string filePath = Path.Combine(folderPath, fileName);
            ////byte[] fileData = _cloudStorageService.GetFileContent(folderPath);
            //byte[] fileData = System.IO.File.ReadAllBytes(folderPath);
            //folderPath = Path.GetDirectoryName(folderPath);
            //folderPath = folderPath.Replace("\\\\", "\\");
            // Fetch the image content from Google Cloud Storage based on imageName

            //byte[] fileData = GeneratePdf();
            //string fileName = "smth.pdf";

            if (fileData != null)
            {
                // Determine the content type based on the file extension
                string contentType = GetFileContentType(fileName);

                if (contentType == "application/pdf")
                {
                    // Return FileStreamResult for PDF files
                    return File(new MemoryStream(fileData), "application/pdf", fileName);
                }

                if (contentType != null)
                {
                    var _file = File(fileData, contentType);
                    return _file;
                }
                else
                {
                    // Handle case where content type is not recognized
                    return BadRequest("Unsupported file format");
                }
            }
            else
            {
                // Handle case where image is not found
                return NotFound();
            }
        }

        private string GetFileContentType(string imageName)
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
                case ".pdf":
                    return "application/pdf";
                // Add additional cases for other supported formats
                default:
                    return null; // Unsupported format
            }
        }

        public byte[] GeneratePdf()
        {
            // Create a MemoryStream to store the generated PDF
            using (MemoryStream ms = new MemoryStream())
            {
                // Create a Document
                iTextSharp.text.Document document = new iTextSharp.text.Document();

                try
                {
                    // Create a PdfWriter to write the content to the MemoryStream
                    PdfWriter writer = PdfWriter.GetInstance(document, ms);

                    // Open the document
                    document.Open();

                    // Add content to the PDF (for example, a simple text)
                    Paragraph paragraph = new Paragraph("Hello, World!");
                    document.Add(paragraph);

                    // Close the document
                    document.Close();

                    // Get the bytes of the PDF content from the MemoryStream
                    byte[] pdfBytes = ms.ToArray();

                    // Return the generated PDF bytes
                    return pdfBytes;
                }
                catch (DocumentException ex)
                {
                    // Handle DocumentException
                    throw ex;
                }
            }
        }
    }
}
