using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IMapper mapper;
        public CoursesController(
            ICourseService courseService,
            ILectureService lectureService,
            ITeacherService teacherService, 
            IMapper mapper)
        {
            this.courseService = courseService;
            this.lectureService = lectureService;
            this.teacherService = teacherService;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details([FromRoute] int id)
        {
            try
            {
                var course = courseService.GetById(id);
                var courseViewModel = mapper.Map<CourseViewModel>(course);
                if (course == null)
                {
                    return NotFound();
                }
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

        [HttpGet("courses/{id}/lectures")]
        public IActionResult Lectures([FromRoute] int id)
        {
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

        [Route("Details/{courseId}")]
        [HttpPost]
        public IActionResult Rate([FromRoute]int courseId, int rating, string feedback)
        {

            int studentId = 1; // TODO

            courseService.RateCourse(courseId, studentId, rating, feedback);

            return RedirectToAction("Details", new { id = courseId });

        }
    }
}
