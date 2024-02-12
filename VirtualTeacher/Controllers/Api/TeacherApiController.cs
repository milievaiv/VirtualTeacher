using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Attributes;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Helpers.Contracts;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.Constants;

namespace VirtualTeacher.Controllers.Api
{
    [ApiController]
    [Route("api/teachers")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService teacherService;
        private readonly IModelMapper modelMapper;

        public TeacherController(ITeacherService teacherService, IModelMapper modelMapper)
        {
            this.teacherService = teacherService;
            this.modelMapper = modelMapper;
        }

        //GET: api/teachers/teacherId
        [AuthorizeApiUsers("teacher, admin")]
        [HttpGet("{teacherId}")]
        public IActionResult GetTeacherById(int teacherId)
        {
            try
            {
                var teacher = teacherService.GetById(teacherId);

                if (teacher == null)
                {
                    return NotFound(Messages.TeacherNotFoundMessage);
                }

                var teacherResponseDto = modelMapper.MapToUserResponseDto(teacher);

                return Ok(teacherResponseDto);
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

        //DELETE: api/teachers/teacherId
        [AuthorizeApiUsers("admin")]
        [HttpDelete("{teacherId}")]
        public IActionResult DeleteTeacher(int teacherId)
        {
            try
            {
                var isDeleted = teacherService.Delete(teacherId);

                if (!isDeleted)
                {
                    return NotFound(Messages.TeacherNotFoundMessage);
                }

                return Ok(string.Format(Messages.TeacherDeletedSuccessfullyMessage, teacherId));
            }
            catch (UnauthorizedOperationException ex)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        //GET: api/teachers/teacherId/courses-created
        [AuthorizeApiUsers("teacher, admin")]
        [HttpGet("{teacherId}/courses-created")]
        public IActionResult GetCoursesCreated(int teacherId)
        {
            try
            {
                var teacher = teacherService.GetById(teacherId);

                if (teacher == null)
                {
                    return NotFound(Messages.TeacherNotFoundMessage);
                }
               
                var coursesCreated = teacherService.GetCoursesCreated(teacher);           
                var coursesDto = modelMapper.MapToCoursesResponseDto(coursesCreated);

                return Ok(coursesDto);
            }
            catch (UnauthorizedOperationException ex)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

    }

}
