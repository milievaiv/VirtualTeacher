using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VirtualTeacher.Attributes;
using VirtualTeacher.Data.Exceptions;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Helpers.Contracts;
using VirtualTeacher.Models.DTO;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Controllers.Api
{
    [AuthorizeApiUsers("teacher", "student")]
    [ApiController]
    [Route("api/users")]
    public class UserApiController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IModelMapper modelMapper;
        public UserApiController(IUserService userService, IModelMapper modelMapper)
        {
            this.userService = userService;
            this.modelMapper = modelMapper;
        }

        //PUT: api/users/userId/change-password
        [HttpPut("{userId}/change-password")]
        public IActionResult ChangePassword([FromRoute] int userId, [FromBody] ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model.NewPassword != model.ConfirmNewPassword)
                return BadRequest("New password and confirmation do not match.");

            try
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value; // Get the email from the JWT token
                var user = userService.GetUserByEmail(email);
                if (user.Id == userId)
                {                  
                    userService.ChangePassword(userId, model.OldPassword, model.NewPassword);
                    return Ok("Password successfully changed.");
                }
                else
                {
                    return StatusCode(StatusCodes.Status405MethodNotAllowed);
                }
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/users/userId/update
        [HttpPut("{userId}/update")]
        public IActionResult UpdateProfile([FromRoute] int userId, [FromBody] UserProfileUpdateModel model)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value; // Get the email from the JWT token
                var user = userService.GetUserByEmail(email);

                if (user.Id == userId)
                {
                    var userWithUpdateInfo = modelMapper.MapToBaseUser(model);
                    var updatedUser = userService.Update(user.Id, userWithUpdateInfo);
                    var userResponseDto = modelMapper.MapToUserResponseDto(updatedUser);

                    return Ok(userResponseDto);
                }
                else
                {
                    return StatusCode(StatusCodes.Status405MethodNotAllowed);
                }
            }
            catch (DuplicateEntityException ex)
            {
                return Conflict(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex);
            }
        }
    }
}
