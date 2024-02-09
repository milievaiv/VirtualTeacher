using Microsoft.AspNetCore.Mvc;
using ReactExample.Exceptions;
using ReactExample.Models;
using ReactExample.Models.DTO;
using ReactExample.Services.Contracts;
using ReactExample.Data.Exceptions;
using ReactExample.Constants;

namespace ReactExample.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ITokenService tokenService;
        private readonly IVerificationService verificationService;

        public AuthApiController(
            ITokenService tokenService,
            IUserService userService, 
            IVerificationService verificationService)
        {
            this.tokenService = tokenService;
            this.userService = userService;
            this.verificationService = verificationService;
        }

        //POST: api/auth/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto registerModel)
        {
            try
            {
                var user = userService.Register(registerModel);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (DuplicateEntityException)
            {
                return this.StatusCode(StatusCodes.Status409Conflict, Messages.UsernameTakenMessage);
            }
        }

        //POST: api/auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginModel)
        {
            try
            {
                var user = verificationService.AuthenticateUser(loginModel);
                string role = DetermineUserRole(user); // Implement this method to determine the role
                string token = tokenService.CreateToken(user, role);
                return Ok(token);
            }
            catch (UnauthorizedOperationException ex)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, Messages.InvalidLoginAttemptMessage);
            }
            catch (EntityNotFoundException)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, Messages.InvalidLoginAttemptMessage);
            }           
        }

        private string DetermineUserRole(BaseUser user)
        {
            switch (user)
            {
                case Admin:
                    return "admin";                
                case Student:
                    return "student";                
                case Teacher:
                    return "teacher";
                default:
                    throw new InvalidOperationException();
            }
        }    
    }
}
