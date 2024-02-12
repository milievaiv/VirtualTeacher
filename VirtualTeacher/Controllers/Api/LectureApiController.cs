using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Attributes;
using VirtualTeacher.Constants;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Helpers;
using VirtualTeacher.Helpers.Contracts;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO.AssignmentDTO;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Controllers.Api
{
    [ApiController]
    [Route("api/lectures")]
    public class LectureController : ControllerBase
    {
        private readonly ILectureService lectureService;
        private readonly IModelMapper modelMapper;

        public LectureController(ILectureService lectureService, IModelMapper modelMapper)
        {
            this.lectureService = lectureService;
            this.modelMapper = modelMapper;
        }

        //POST: api/lectures/lectureId/assignments
        [AuthorizeApiUsers("teacher")]
        [HttpPost("{lectureId}/assignments")]
        public IActionResult AddAssignmentToLecture(int lectureId, [FromBody] AssignmentDto newAssignmentDto)
        {
            try
            {
                var newAssignment = modelMapper.MapToAssignment(newAssignmentDto);
                lectureService.AddAssignmentToLecture(lectureId, newAssignment);

                return this.StatusCode(StatusCodes.Status200OK, Messages.AssignmentAddedSuccessMessage);
            }
            catch (EntityNotFoundException ex)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (DuplicateEntityException ex)
            {
                return this.StatusCode(StatusCodes.Status409Conflict, ex.Message);
            }
        }
    }
}
