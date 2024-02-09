using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.Constants;
using VirtualTeacher.Data.Exceptions;
using VirtualTeacher.Models.DTO.AuthenticationDTO;

namespace VirtualTeacher.Controllers.Api
{
    [Route("api/admin")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private readonly IAdminService adminService;
        public AdminApiController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        //POST: api/admin/register
        [HttpPost("register")]
        public IActionResult RegisterAdmin([FromBody] RegisterDto registerModel)
        {
            try
            {
                var admin = adminService.Register(registerModel);
                return this.StatusCode(StatusCodes.Status200OK, admin);
            }
            catch (DuplicateEntityException)
            {
                return this.StatusCode(StatusCodes.Status409Conflict, Messages.EmailTakenErrorMessage);
            }
        }

        //POST: api/admin/approve-teacher
        [HttpPost("approve-teacher")]
        public IActionResult ApproveTeacher([FromBody] string email)
        {
            var approvedTeacher = adminService.ApproveTeacher(email);
            return this.StatusCode(StatusCodes.Status200OK, approvedTeacher);
        }
    }
}
