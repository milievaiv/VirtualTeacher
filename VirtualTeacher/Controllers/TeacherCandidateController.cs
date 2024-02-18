using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Constants;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Models.DTO.AuthenticationDTO;
using VirtualTeacher.Models.DTO.TeacherDTO;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Controllers
{
    public class TeacherCandidateController : Controller
    {
        private readonly ITeacherCandidateService teacherCandidateService;
        private readonly IUserService userService;
        private readonly IEmailService emailService;

        public TeacherCandidateController(
        ITeacherCandidateService teacherCandidateService,
        IEmailService emailService,
        IUserService userService)
        {
            this.teacherCandidateService = teacherCandidateService;
            this.emailService = emailService;
            this.userService = userService;
        }

        [HttpPost("register")]
        public IActionResult RegisterTeacher([FromForm] RegisterDto registerModel)
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
        public IActionResult Index()
        {
            bool? applicationSuccess = TempData["applicationSuccess"] as bool?;
            TeacherCandidateDto teacherCandidateDto = new TeacherCandidateDto();

            // Pass both the applicationSuccess boolean value and the TeacherCandidateDto object to the view
            ViewData["applicationSuccess"] = applicationSuccess;
            return View(teacherCandidateDto);
        }
        //POST: api/teacher-candidates
        [HttpPost]
        public IActionResult Apply([FromForm] TeacherCandidateDto teacherCandidateDto)
        {
            try
            {
                if (!teacherCandidateService.FiveDaysPastApplication(teacherCandidateDto.Email))
                {
                    throw new DuplicateEntityException("Please wait at least 5 days until your next submission.");
                }
                var requestId = teacherCandidateService.ProcessSubmission(teacherCandidateDto);
                emailService.SendVerificationEmail(requestId, teacherCandidateDto);
                TempData["applicationSuccess"] = true;

                //return this.StatusCode(StatusCodes.Status200OK, Messages.VerificationEmailSentMessage);
            }
            catch
            {
                TempData["applicationSuccess"] = false;
                //ModelState.AddModelError(string.Empty, ex.Message); // Add model-level error
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("verify-application")]
        public async Task<IActionResult> VerifyApplication([FromQuery] string requestId)
        {
            try
            {
                //teacherCandidateService.SaveVerifiedApplication(requestId);

                await emailService.VerifyApplication(requestId);

                return RedirectToAction("VerificationSuccess");
            }
            catch (DuplicateEntityException)
            {
                return RedirectToAction("VerificationFailure");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        public IActionResult VerificationSuccess()
        {
            // Render a view indicating verification success
            return View("VerificationSuccess");
        }

        public IActionResult VerificationFailure()
        {
            // Render a view indicating verification failure
            return View("VerificationFailure");
        }
    }
}
