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

namespace VirtualTeacher.Controllers.Api
{
    [AuthorizeApiUsers("teacher", "student")]
    [ApiController]
    [Route("api/courses")]
    public class CoursesApiController : ControllerBase
    {
        private readonly ICourseService courseService;
        private readonly ITeacherService teacherService;
        private readonly IAdminService adminService;

        public CoursesApiController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        // GET: api/courses
        [HttpGet]
        public ActionResult<ICollection<CourseDto>> GetCourses()
        {
            try
            {
                return Ok(courseService.GetCourses());
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: api/courses/{id}
        [HttpGet("{id}")]
        public ActionResult<CourseDto> GetCourseById(int id)
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

        // POST: api/courses
        [HttpPost]
        public ActionResult<CourseDto> CreateCourse([FromBody]CreateCourseModel createCourseModel)
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

        // PUT: api/courses/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateCourse(int id, CourseDto courseDto)
        {
            // Validate and update the course in the database

            return NoContent();
        }

        // DELETE: api/courses/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteCourse(int id)
        {
            // Delete the course from the database

            return NoContent();
        }
    }
}
