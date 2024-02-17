using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VirtualTeacher.Attributes;
using VirtualTeacher.Services;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly CloudStorageService _cloudStorageService;
        private readonly IStudentService studentService;
        private readonly IAssignmentService assignmentService;

        public AssignmentController(CloudStorageService cloudStorageService, IAssignmentService assignmentService, IStudentService studentService)
        {
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
    }
}
