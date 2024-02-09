using Microsoft.AspNetCore.Mvc;
using ReactExample.Attributes;
using ReactExample.Constants;
using ReactExample.Data.Exceptions;
using ReactExample.Exceptions;
using ReactExample.Helpers;
using ReactExample.Helpers.Contracts;
using ReactExample.Models;
using ReactExample.Models.DTO.AssignmentDTO;
using ReactExample.Services.Contracts;

namespace ReactExample.Controllers.Api
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
