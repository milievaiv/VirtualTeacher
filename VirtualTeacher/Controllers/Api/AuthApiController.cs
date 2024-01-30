using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO;
using VirtualTeacher.Services;
using VirtualTeacher.Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using PhotoForum.Controllers.Data.Exceptions;

namespace VirtualTeacher.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {
        private readonly IStudentService studentService;
        private readonly ITokenService tokenService;
        private readonly IVerificationService verificationService;

        public AuthApiController(ITokenService tokenService, IStudentService studentService, IVerificationService verificationService)
        {
            this.tokenService = tokenService;
            this.studentService = studentService;
            this.verificationService = verificationService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel registerModel)
        {
            try
            {
                var user = studentService.Register(registerModel);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (DuplicateEntityException)
            {
                return Conflict("That username is taken.Try another.");
            }
            catch (DuplicateEmailException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            try
            {
                var user = verificationService.AuthenticateUser(loginModel);
                string role = DetermineUserRole(user); // Implement this method to determine the role
                string token = tokenService.CreateToken(user, role);
                return Ok(token);
            }
            catch (UnauthorizedOperationException)
            {
                return BadRequest("Invalid login attempt!");
            }
            catch(EntityNotFoundException)
            { 
                return BadRequest("Invalid login attempt!");
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
