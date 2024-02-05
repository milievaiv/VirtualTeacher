using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO;
using VirtualTeacher.Attributes;
using VirtualTeacher.Data.Exceptions;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Constants;
using VirtualTeacher.Helpers.Contracts;
using VirtualTeacher.Models.DTO.CourseDTO;

namespace VirtualTeacher.Controllers.Api
{
    [ApiController]
    [Route("api/courses")]
    public class CoursesApiController : ControllerBase
    {
        private readonly ICourseService courseService;
        private readonly ITeacherService teacherService;
        private readonly IModelMapper modelMapper;

        public CoursesApiController(
            ICourseService courseService,
            ITeacherService teacherService,
            IModelMapper modelMapper)
        {
            this.courseService = courseService;
            this.teacherService = teacherService;
            this.modelMapper = modelMapper;
        }

        //GET: api/courses/drafts
        [AuthorizeApiUsers("teacher, admin")]
        [HttpGet("drafts")]
        public IActionResult GetDrafts()
        {
            try
            {
                var role = User.FindFirst(ClaimTypes.Role)?.Value;
                var courses = courseService.GetAll();
                if (role == UserRole.Student.ToString())
                {
                    courses = courses.Where(x => x.IsPublic).ToList();
                }
                
                var drafts = courses.Select(course => modelMapper.MapToCourseResponseDto(course)).ToList();
                return Ok(drafts);
            }
            catch (EntityNotFoundException ex)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
        }

        //GET: api/courses/search/{title}
        [HttpGet("search/{title}")]
        public IActionResult GetCoursesByTitle([FromRoute] string title)
        {
            try
            {
                var role = User.FindFirst(ClaimTypes.Role)?.Value;
                var courses = courseService.GetAll().Where(x => x.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();

                if (role == UserRole.Student.ToString())
                {
                    courses = courses.Where(x => x.IsPublic).ToList();
                }
                var result = courses.Select(course => modelMapper.MapToCourseResponseDto(course)).ToList();
                return Ok(result);
            }
            catch (EntityNotFoundException ex)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
        }

        //GET: api/courses
        [HttpGet("")]
        public IActionResult GetCourses()
        {
            try
            {
                var role = User.FindFirst(ClaimTypes.Role)?.Value;
                var courses = courseService.GetAll();
                if (role == UserRole.Student.ToString())
                {
                    courses = courses.Where(x => x.IsPublic).ToList();
                }
                var courseDtos = courses.Select(course => modelMapper.MapToCourseResponseDto(course)).ToList();
                return Ok(courseDtos);
            }
            catch (EntityNotFoundException ex)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
        }

        //GET: api/courses/id
        [AuthorizeApiUsers("teacher, admin")]
        [HttpGet("{id}")]
        public IActionResult GetCourseById(int id)
        {
            try
            {
                var course = courseService.GetById(id);
                var courseDto = modelMapper.MapToCourseResponseDto(course); 

                return Ok(courseDto);
            }
            catch (EntityNotFoundException ex)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
        }
        //DELETE: api/courses/id
        [AuthorizeApiUsers("teacher, admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteCourse(int id)
        {
            
            try
            {
                var course = courseService.GetById(id);
                return Ok(courseService.Delete(course));
                throw new InvalidOperationException();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
        //POST: api/courses
        [AuthorizeApiUsers("teacher, admin")]
        [HttpPost("")]
        public IActionResult CreateCourse([FromBody] CreateCourseDto createCourseModel)
        {

            try
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;
                if (email != null)
                {
                    var teacher = teacherService.GetByEmail(email);
                    var course = courseService.Create(createCourseModel, teacher);
                    var courseDto = modelMapper.MapToCourseResponseDto(course);

                    return Ok(courseDto);
                }
                throw new InvalidOperationException();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        //PUT: api/courses/courseId
        [AuthorizeApiUsers("teacher, admin")]
        [HttpPut("{courseId}")]
        public IActionResult UpdateCourse(int courseId, [FromBody] UpdateCourseDto updateCourseDto)
        {
            try
            {
                var updatedCourse = modelMapper.MapToUpdateCourse(updateCourseDto);
                courseService.Update(courseId, updatedCourse);
                return this.StatusCode(StatusCodes.Status200OK, Messages.CourseUpdatedSuccessfullyMessage);
            }
            catch (ApplicationException ex)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        //POST: api/courses/courseId/publicize
        [AuthorizeApiUsers("teacher, admin")]
        [HttpPost("{courseId}/publicize")]
        public IActionResult PublicizeCourse(int courseId)
        {
            try
            {
                courseService.PublicizeCourse(courseId);
                return this.StatusCode(StatusCodes.Status200OK, Messages.CoursePublicizedSuccessfullyMessage);
            }
            catch (ApplicationException ex)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        //POST: api/courses/courseId/mark-as-draft
        [AuthorizeApiUsers("teacher, admin")]
        [HttpPost("{courseId}/mark-as-draft")]
        public IActionResult MarkAsDraft(int courseId)
        {
            try
            {
                courseService.MarkAsDraft(courseId);
                return this.StatusCode(StatusCodes.Status200OK, Messages.CourseMarkedAsDraftSuccessfullyMessage);
            }
            catch (ApplicationException ex)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        //GET: api/courses/created
        [AuthorizeApiUsers("teacher, admin")]
        [HttpGet("created")]
        public ActionResult<CourseTopicDto> GetCoursesCreated()
        // FromQuery has to be tested
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;
                if (email != null)
                {
                    var teacher = teacherService.GetByEmail(email);
                    var courses = teacherService.GetCoursesCreated(teacher);
                    var courseDtos = courses.Select(course => modelMapper.MapToCourseResponseDto(course)).ToList();

                    return Ok(courseDtos);
                }
                throw new InvalidOperationException();
            }
            catch (DuplicateEntityException ex)
            {
                return this.StatusCode(StatusCodes.Status409Conflict, ex.Message);
            }
        }

        //POST: api/courses/courseId/lectures
        [AuthorizeApiUsers("teacher, student")] 
        [HttpPost("{courseId}/lectures")]
        public IActionResult AddLectureToCourse(int courseId, [FromBody] LectureDto newLectureDto)
        {
            try
            {
                var newLecture = modelMapper.MapToLectue(newLectureDto);
                courseService.AddLectureToCourse(courseId, newLecture);

                return this.StatusCode(StatusCodes.Status200OK, Messages.LectureAddedSuccessfullyMessage);
            }
            catch (EntityNotFoundException ex)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
        }
    }
}
