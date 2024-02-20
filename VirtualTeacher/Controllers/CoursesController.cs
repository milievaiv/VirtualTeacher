using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using VirtualTeacher.Attributes;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Models;
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

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            var courses = courseService.GetAll().ToList();
            List<CourseViewModel> viewModels = mapper.Map<List<Course>, List<CourseViewModel>>(courses);

            return View(viewModels);
        }


        [AllowAnonymous]
        [HttpGet("courses/GetCoursesByTopic")]
        public IActionResult GetCoursesByTopic(string topic)
        {
            var allCourses = courseService.GetAll();

            IEnumerable<Course> filteredCourses = allCourses
                .Where(course => string.Equals(topic, "All Courses", StringComparison.OrdinalIgnoreCase) ||
                                 course.CourseTopic.Topic.Equals(topic, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(course => course.Ratings.Any() ? course.Ratings.Average(r => r.RatingValue) : 0);

            var viewModels = mapper.Map<List<CourseViewModel>>(filteredCourses.ToList());

            return PartialView("_CoursesByTopicPartial", viewModels);
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
                    return View("NotFoundError");
                }

                var topCourses = courseService.GetAll()
                .OrderByDescending(x => x.Ratings.Any() ? x.Ratings.Average(r => r.RatingValue) : 0)
                .ToList();

                var courseViewModel = mapper.Map<CourseViewModel>(course);
                courseViewModel.TopCourses = mapper.Map<IEnumerable<CourseViewModel>>(topCourses);

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
                return View("NotFoundError");
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
                    return View("NotFoundError");
                }

                return View(courseViewModel);
            }
            catch (EntityNotFoundException)
            {
                return View("NotFoundError");
            }
        }

        //[HttpPost]
        //public ActionResult IncrementProgress(int courseId)
        //{
        //    var user = HttpContext.User;
        //    var email = user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
        //    var student = studentService.GetByEmail(email);
        //    var course = courseService.GetById(courseId);

        //    double? currentProgress = studentService.CalculateProgress(student, course);
        //    currentProgress++;

        //    return RedirectToAction("Lectures", new { courseId = courseId });
        //}


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
