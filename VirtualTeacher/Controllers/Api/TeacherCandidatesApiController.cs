using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.Models.DTO.AuthenticationDTO;
using VirtualTeacher.Models.DTO.TeacherDTO;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Constants;
using VirtualTeacher.Data.Exceptions;
using VirtualTeacher.Models.DTO.AuthenticationDTO;
using VirtualTeacher.Models.DTO.TeacherDTO;

namespace VirtualTeacher.Controllers.Api
{
    [ApiController]
    [Route("api/teacher-candidates")]
    public class TeacherCandidatesController : ControllerBase
    {
        private readonly ITeacherCandidateService teacherCandidateService;
        private readonly IUserService userService;
        private readonly IEmailService emailService;

        public TeacherCandidatesController(
            ITeacherCandidateService teacherCandidateService,
            IEmailService emailService,
            IUserService userService)
        {
            this.teacherCandidateService = teacherCandidateService;
            this.emailService = emailService;
            this.userService = userService;
        }

        //POST: api/teacher-candidates/register
        [HttpPost("register")]
        public IActionResult RegisterTeacher([FromBody] RegisterDto registerModel)
        {
            try
            {
                var teacher = userService.Register(registerModel);
                return Ok(teacher);
            }
            catch (DuplicateEntityException)
            {
                return this.StatusCode(StatusCodes.Status409Conflict, Messages.EmailTakenErrorMessage);
            }
        }

        //POST: api/teacher-candidates
        [HttpPost("")]
        public IActionResult SubmitApplication([FromBody] TeacherCandidateDto teacherCandidateDto)
        {
            try
            {
                // Process submission and send verification email
                var requestId = teacherCandidateService.ProcessSubmission(teacherCandidateDto);
                emailService.SendVerificationEmail(requestId, teacherCandidateDto);

                return this.StatusCode(StatusCodes.Status200OK, Messages.VerificationEmailSentMessage);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        //GET: api/apply-for-teacher?requestId={requestId}
        [HttpGet]
        [Route("verify-submission")]
        public async Task<IActionResult> VerifyApplication([FromQuery] string requestId)
        {
            try
            {
                await emailService.VerifyApplication(requestId);

                return this.StatusCode(StatusCodes.Status200OK, Messages.ApplicationVerifiedMessage);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
    }
}
