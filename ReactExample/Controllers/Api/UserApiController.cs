using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ReactExample.Attributes;
using ReactExample.Data.Exceptions;
using ReactExample.Exceptions;
using ReactExample.Helpers.Contracts;
using ReactExample.Models.DTO;
using ReactExample.Services.Contracts;
using ReactExample.Constants;

namespace ReactExample.Controllers.Api
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

        //GET: api/users
        [HttpGet("")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = userService.GetAll();
                var userDtos = users.Select(user => modelMapper.MapToUserResponseDto(user)).ToList();

                return StatusCode(StatusCodes.Status200OK, userDtos);
            }
            catch (EntityNotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound, Messages.NoUsersMessage);
            }
            catch (UnauthorizedOperationException)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }

        //GET: api/users/userId
        [HttpGet("{userId}")]
        public IActionResult GetUserById(int userId)
        {
            try
            {
                var user = userService.GetById(userId);

                if (user == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, Messages.UserNotFound);
                }
                var userDto = modelMapper.MapToUserResponseDto(user);

                return Ok(userDto);
            }
            catch (UnauthorizedOperationException)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            catch (EntityNotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound, Messages.UserNotFound);
            }
        }

        //PUT: api/users/userId/change-password
        [HttpPut("{userId}/change-password")]
        public IActionResult ChangePassword([FromRoute] int userId, [FromBody] ChangePasswordDto model)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);

            if (model.NewPassword != model.ConfirmNewPassword)
                return StatusCode(StatusCodes.Status400BadRequest, Messages.PasswordConfirmationMismatchMessage);

            try
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value; // Get the email from the JWT token
                var user = userService.GetByEmail(email);
                if (user.Id == userId)
                {                  
                    userService.ChangePassword(userId, model.OldPassword, model.NewPassword);
                    return StatusCode(StatusCodes.Status200OK, Messages.PasswordChangeSuccessMessage);
                }
                else
                {
                    return StatusCode(StatusCodes.Status405MethodNotAllowed);
                }
            }
            catch (EntityNotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound, Messages.UserNotFound);
            }
            catch (UnauthorizedOperationException)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            catch (ArgumentException)
            {
                return StatusCode(StatusCodes.Status405MethodNotAllowed);
            }
        }

        // PUT: api/users/userId/update
        [HttpPut("{userId}/update")]
        public IActionResult UpdateProfile([FromRoute] int userId, [FromBody] UserProfileUpdateDto model)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value; // Get the email from the JWT token
                var user = userService.GetByEmail(email);

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
                return this.StatusCode(StatusCodes.Status409Conflict, ex.Message);
            }
            catch (EntityNotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound, Messages.UserNotFound);
            }
            catch (UnauthorizedOperationException)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }
    }
}
