using AutoMapper;
using Google.Apis.Drive.v3.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using VirtualTeacher.Attributes;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO.CourseDTO;
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
        private readonly ICourseTopicService courseTopicService;
        private readonly IMapper mapper;
        private readonly CloudStorageService cloudStorageService;
        public CoursesController(
            ICourseService courseService,
            ILectureService lectureService,
            ITeacherService teacherService,
            IUserService userService,
            IStudentService studentService,
            ICourseTopicService courseTopicService,
            IMapper mapper,
            CloudStorageService cloudStorageService
            )
        {
            this.courseService = courseService;
            this.lectureService = lectureService;
            this.teacherService = teacherService;
            this.userService = userService;
            this.studentService = studentService;
            this.courseTopicService = courseTopicService;
            this.mapper = mapper;
            this.cloudStorageService = cloudStorageService;

        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            var courses = courseService.GetAll().ToList();
            List<CourseViewModel> viewModels = mapper.Map<List<Course>, List<CourseViewModel>>(courses);

            return View(viewModels);
        }

        [AuthorizeUsers("teacher")]
        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new CourseCreateViewModel();
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> CoursePicture(int courseId)
        {
            var course = courseService.GetById(courseId);
            var pfpName = course.Id + ".png";

            // Load profile picture data
            byte[] imageData = cloudStorageService.GetImageContent(pfpName, "course-pictures/");

            return File(imageData, "image/png");
        }

        [AuthorizeUsers("teacher")]
        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateViewModel model)
        {

            var user = HttpContext.User;
            var email = user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
            var teacher = teacherService.GetByEmail(email);

            if (ModelState.IsValid)
            {
                var course = mapper.Map<Course>(model);

                var coursetopic = courseTopicService.GetAll().FirstOrDefault(x => x.Topic == model.CourseTopic);
                if (coursetopic == null)
                {
                    ModelState.AddModelError("CourseTopic", "Course topic not found.");
                    // Populate any required data for the view here before returning
                    return View(model);
                }

                var courseDto = new CreateCourseDto
                {
                    Title = course.Title,
                    Description = course.Description,
                    StartDate = course.StartDate,
                    CourseTopicId = coursetopic.Id,
                };

                var _course = courseService.Create(courseDto, teacher);

                string newFileName = _course.Id + ".png";
                if (model.Photo != null && model.Photo.Length > 0)
                {
                    using (var fileStream = model.Photo.OpenReadStream())
                    {
                        // Use the dynamically fetched bucket name
                        await cloudStorageService.UploadFileAsync("course-pictures/" + newFileName, fileStream);
                    }
                }

                return RedirectToAction("Index", "Courses");
            }

            return View(model);
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

        [AllowAnonymous]
        [HttpGet("courses/{id}/details")]
        public IActionResult Details([FromRoute] int id)
        {
            Student student = null;
            var jwtFromRequest = Request.Cookies["Authorization"];

            if (!string.IsNullOrEmpty(jwtFromRequest))
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                try
                {
                    var jwtToken = tokenHandler.ReadToken(jwtFromRequest) as JwtSecurityToken;
                    if (jwtToken != null)
                    {
                        var emailClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                        if (emailClaim != null)
                        {
                            student = studentService.GetByEmail(emailClaim.Value);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // This ensures that any issues with token processing do not prevent the method from proceeding
                }
            }

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

                if (course.Ratings != null && course.Ratings.Any())
                {
                    courseViewModel.AverageRating = course.Ratings.Average(r => r.RatingValue);
                }

                if (student != null)
                {
                    bool isEnrolled = student.EnrolledCourses.Any(x => x.CourseId == id);
                    ViewBag.IsEnrolled = isEnrolled;
                   
                }              

                ViewBag.CourseId = id; 

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

            courseService.RateCourse(courseId, student.Id, rating, feedback);

            return RedirectToAction("Details", new { id = courseId });     

        }       
    }
}
