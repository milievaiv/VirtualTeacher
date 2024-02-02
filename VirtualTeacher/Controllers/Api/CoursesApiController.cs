using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using VirtualTeacher.Data;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO;
using VirtualTeacher.Attributes;
using System.Security.Claims;
using VirtualTeacher.Data.Exceptions;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Services;

namespace VirtualTeacher.Controllers.Api
{
    [ApiController]
    [Route("api/courses")]
    public class CoursesApiController : ControllerBase
    {
        private readonly ICourseService courseService;
        private readonly ITeacherService teacherService;
        private readonly IAdminService adminService;

        public CoursesApiController(ICourseService courseService, ITeacherService teacherService)
        {
            this.courseService = courseService;
            this.teacherService = teacherService;

        }

        [AuthorizeApiUsers("teacher, admin")]
        [HttpGet("drafts")]
        public IActionResult GetDrafts()
        {
            try
            {
                var role = User.FindFirst(ClaimTypes.Role)?.Value;
                var courses = courseService.GetCourses();
                if (role == UserRole.Student.ToString())
                {
                    courses = courses.Where(x => x.IsPublic).ToList();
                }
                return Ok(courses);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("search/{title}")]
        public IActionResult GetCoursesByTitle([FromQuery] string title)
        {
            try
            {
                var role = User.FindFirst(ClaimTypes.Role)?.Value;
                var courses = courseService.GetCoursesByTitle(title);
                if (role == UserRole.Student.ToString())
                {
                    courses = courses.Where(x => x.IsPublic).ToList();
                }
                return Ok(courses);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetCourses()
        {
            try
            {
                var role = User.FindFirst(ClaimTypes.Role)?.Value;
                var courses = courseService.GetCourses();
                if (role == UserRole.Student.ToString())
                {
                    courses = courses.Where(x => x.IsPublic).ToList();
                }        
                return Ok(courses);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [AuthorizeApiUsers("teacher, admin")]
        [HttpGet("{id}")]
        public IActionResult GetCourseById(int id)
        {
            try
            {
                return Ok(courseService.GetCourseById(id));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [AuthorizeApiUsers("teacher, admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteCourse(int id)
        {
            
            try
            {
                var course = courseService.GetCourseById(id);
                return Ok(courseService.DeleteCourse(course));
                throw new InvalidOperationException();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AuthorizeApiUsers("teacher")]
        [HttpPost]
        public IActionResult CreateCourse([FromBody] CreateCourseModel createCourseModel)
        {

            try
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;
                if (email != null)
                {
                    var teacher = teacherService.GetTeacherByEmail(email);
                    return Ok(courseService.CreateCourse(createCourseModel, teacher));
                }
                throw new InvalidOperationException();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AuthorizeApiUsers("teacher")]
        [HttpPut("{id}")]
        public IActionResult UpdateCourse(int id, CourseDto courseDto)
        {
            // Validate and update the course in the database

            return NoContent();
        }

        [AuthorizeApiUsers("teacher, admin")]
        [HttpGet("topic/{id}")]
        public ActionResult<CourseTopicDto> GetCourseTopicById(int id)
        {
            try
            {
                return Ok(courseService.GetCourseTopicById(id));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [AuthorizeApiUsers("teacher")]
        [HttpPost("topics")]
        public ActionResult<CourseTopicDto> CreateCourseTopic([FromBody] CourseTopicCreationRequest topic)
        // FromQuery has to be tested
        {
            try
            {
                var result = topic.Topic;
                return Ok(courseService.CreateCourseTopic(result));
            }
            catch (DuplicateEntityException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}
