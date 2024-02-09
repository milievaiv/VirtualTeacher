using Microsoft.AspNetCore.Mvc;
using ReactExample.Attributes;
using ReactExample.Data.Exceptions;
using ReactExample.Exceptions;
using ReactExample.Helpers.Contracts;
using ReactExample.Services.Contracts;
using ReactExample.Constants;

namespace ReactExample.Controllers.Api
{
    [AuthorizeApiUsers("student")]
    [ApiController]
    [Route("api/students")]
    public class StudentApiController : ControllerBase
    {
        private readonly IStudentService studentService;
        private readonly IModelMapper modelMapper;
        private readonly ICourseService courseService;

        public StudentApiController(
            IStudentService studentService, 
            IModelMapper modelMapper,
            ICourseService courseService)
        {
            this.studentService = studentService;
            this.modelMapper = modelMapper;
            this.courseService = courseService;
        }

        //GET: api/students
        [HttpGet("")]
        public IActionResult GetStudents()
        {
            try
            {
                var students = studentService.GetAll();
                var studentsDto = modelMapper.MapToUserResponseDtoList(students);

                return Ok(studentsDto);
            }
            catch (UnauthorizedOperationException ex)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        //GET: api/students/studentId
        [HttpGet("{studentId}")]
        public IActionResult GetStudentById(int studentId)
        {
            try
            {
                var student = studentService.GetById(studentId);

                if (student == null)
                {
                    return this.StatusCode(StatusCodes.Status404NotFound, Messages.StudentNotFoundMessage);
                }

                var studentDto = modelMapper.MapToUserResponseDto(student);

                return Ok(studentDto);
            }
            catch (UnauthorizedOperationException ex)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [AuthorizeApiUsers("admin")]
        //DELETE: api/students/studentId
        [HttpDelete("{studentId}")]
        public IActionResult DeleteStudent(int studentId)
        {
            try
            {
                var isDeleted = studentService.Delete(studentId);

                if (!isDeleted)
                {
                    return this.StatusCode(StatusCodes.Status404NotFound, Messages.StudentNotFoundMessage);
                }

                return this.StatusCode(StatusCodes.Status200OK, Messages.StudentDeletionSuccessMessage);
            }
            catch (UnauthorizedOperationException ex)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }            
        }

        //POST: api/students/studentId/enroll/courseId
        //[HttpPost("{studentId}/enroll/{courseId}")]
        //public IActionResult EnrollInCourse(int studentId, int courseId)
        //{
        //    try
        //    {
        //        var student = studentService.GetById(studentId);
        //        var course = courseService.GetById(courseId);

        //        if (student == null || course == null)
        //        {
        //            return this.StatusCode(StatusCodes.Status404NotFound, Messages.StudentCourseNotFoundMessage);
        //        }

        //        studentService.EnrollInCourse(student, course);

        //        return this.StatusCode(StatusCodes.Status200OK, Messages.EnrollmentSuccessMessage);
        //    }
        //    catch (UnauthorizedOperationException ex)
        //    {
        //        return this.StatusCode(StatusCodes.Status401Unauthorized, ex.Message);
        //    }
        //    catch (EntityNotFoundException ex)
        //    {
        //        return this.StatusCode(StatusCodes.Status404NotFound, ex.Message);
        //    }
        //    catch (DuplicateEntityException ex)
        //    {
        //        return this.StatusCode(StatusCodes.Status409Conflict, ex.Message);
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        return this.StatusCode(StatusCodes.Status405MethodNotAllowed, ex.Message);
        //    }
        //}
    }
}
