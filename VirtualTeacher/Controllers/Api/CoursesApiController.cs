using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using VirtualTeacher.DTOs;
using System.Collections.Generic;
using System.Linq;
using VirtualTeacher.Data;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO;
using VirtualTeacher.Attributes;

namespace VirtualTeacher.Controllers.Api
{
    [AuthorizeApiUsers("teacher", "student")]
    [ApiController]
    [Route("api/courses")]
    public class CoursesApiController : ControllerBase
    {
        private readonly ICourseService courseService;

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
                return Ok(courseService.CreateCourse(createCourseModel));
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
