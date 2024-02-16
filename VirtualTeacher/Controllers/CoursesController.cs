using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using VirtualTeacher.Attributes;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Models.ViewModel.CourseViewModel;
using VirtualTeacher.Services;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService courseService;
        private readonly ILectureService lectureService;
        private readonly ITeacherService teacherService;
        private readonly IUserService userService;
        private readonly IStudentService studentService;
        private readonly IMapper mapper;
        public CoursesController(
            ICourseService courseService,
            ILectureService lectureService,
            ITeacherService teacherService,
            IUserService userService,
            IStudentService studentService,
        IMapper mapper)
        {
            this.courseService = courseService;
            this.lectureService = lectureService;
            this.teacherService = teacherService;
            this.userService = userService;
            this.studentService = studentService;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        [AuthorizeUsers("student")]
        [HttpGet("courses/{id}/details")]
        public IActionResult Details([FromRoute] int id)
        {
            var user = HttpContext.User;
            var email = user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
            var student = studentService.GetByEmail(email);

            try
            {
                var course = courseService.GetById(id);               

                if (course == null)
                {
                    return NotFound();
                }
                var courseViewModel = mapper.Map<CourseViewModel>(course);

                bool isEnrolled = student.EnrolledCourses.Any(x => x.CourseId == id);
                ViewBag.IsEnrolled = isEnrolled;
                ViewBag.CourseId = id; 

                if (course.Ratings != null && course.Ratings.Any())
                {
                    courseViewModel.AverageRating = course.Ratings.Average(r => r.RatingValue);
                }

                return View(courseViewModel);
            }
            catch (EntityNotFoundException)
            {
                return NoContent();
            }
        }

        [AuthorizeUsers("student")]
        [HttpGet("courses/{id}/lectures")]
        public IActionResult Lectures([FromRoute] int id)
        {
            var user = HttpContext.User;
            var email = user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
            var student = studentService.GetByEmail(email);

            bool isEnrolled = student.EnrolledCourses.Any(x => x.CourseId == id);

            //Test
            if (!isEnrolled)
            {
                return RedirectToAction("Details", new { id });
            }

            try
            {
                var course = courseService.GetById(id);
                var courseViewModel = mapper.Map<CourseViewModel>(course);
                if (course == null)
                {
                    return NotFound();
                }

                return View(courseViewModel);
            }
            catch (EntityNotFoundException)
            {
                return NoContent();
            }

        }               
        
        [AuthorizeUsers("student")]
        [HttpGet]
        public IActionResult Enroll([FromRoute] int id)
         {
            var user = HttpContext.User;
            var email = user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
            var student = studentService.GetByEmail(email);
           
            try
            {
                courseService.EnrollStudentInCourse(student.Id, id);

                return RedirectToAction("Details", new { id });
            }
            catch (Exception)
            {
                return RedirectToAction("Details", new { id, errorMessage = "Could not enroll in course. Please try again." });
            }
        }

        [AuthorizeUsers("student")]
        [HttpPost("details/{courseId}/rate")]
        public IActionResult Rate([FromRoute]int courseId, int rating, string feedback)
        {
            var user = HttpContext.User;
            var email = user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
            var student = studentService.GetByEmail(email);
            // var isCourseCompleted = student.CompletedCourses.Any(x => x.CourseId == courseId);

            courseService.RateCourse(courseId, student.Id, rating, feedback);
           // return Json(new { success = true, responseText = "Your feedback has been submitted!" });
           return RedirectToAction("Details", new { id = courseId });

            //if (isCourseCompleted)
            //{
               
            //}
            //else
            //{
            //    return Conflict("You must have completed the course to rate it.");
            //}          

        }
    }
}
