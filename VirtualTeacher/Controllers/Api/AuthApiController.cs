using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO.AuthenticationDTO;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.Constants;
using VirtualTeacher.Exceptions;

namespace VirtualTeacher.Controllers.Api
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
                return StatusCode(StatusCodes.Status409Conflict, Messages.UsernameTakenMessage);
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
                Response.Cookies.Append("Authorization", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

                if (role == "student")
                {
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Index", "Home");
            }
            catch (UnauthorizedOperationException ex)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, Messages.InvalidLoginAttemptMessage);
            }
            catch (EntityNotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound, Messages.InvalidLoginAttemptMessage);
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
