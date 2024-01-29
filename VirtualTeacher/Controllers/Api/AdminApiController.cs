using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.Exceptions;

namespace VirtualTeacher.Controllers.Api
{
    [Route("api/Admin")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private readonly IStudentService studentService;
        private readonly IAdminService adminService;
        public AdminApiController(IStudentService studentService, IAdminService adminService)
        {
            this.studentService = studentService;
            this.adminService = adminService;
        }

        [HttpPost("register")]
        public IActionResult RegisterAdmin([FromBody] RegisterModel registerModel)
        {
            try
            {
                var admin = adminService.Register(registerModel);
                return Ok(admin);
            }
            catch (DuplicateEntityException)
            {

                return Conflict("That email is taken. Try another.");
            }
        }
    }
}
