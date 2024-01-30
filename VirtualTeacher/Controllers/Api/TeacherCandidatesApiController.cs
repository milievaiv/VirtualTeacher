﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO;

namespace VirtualTeacher.Controllers.Api
{
    [ApiController]
    [Route("api/teacher-candidates")]
    public class TeacherCandidatesController : ControllerBase
    {
        private readonly ITeacherCandidateService _teacherCandidateService;
        private readonly IEmailService _emailService;

        public TeacherCandidatesController(ITeacherCandidateService teacherCandidateService, IEmailService emailService)
        {
            _teacherCandidateService = teacherCandidateService;
            _emailService = emailService;
        }

        [HttpPost]
        public IActionResult SubmitApplication([FromBody] TeacherCandidate teacherCandidateDto)
        {
            try
            {
                // Process submission and send verification email
                var requestId = _teacherCandidateService.ProcessSubmission(teacherCandidateDto);
                _emailService.SendVerificationEmail(requestId, teacherCandidateDto);

                return Ok("Verification email sent. Please check your email to complete the application process.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //localhost:5267/api/apply-for-teacher?requestId={requestId}
        [HttpGet]
        [Route("verify-submission")]
        public IActionResult VerifyApplication([FromQuery] string requestId)
        {
            try
            {
                _emailService.FindEmail(requestId);

                return Ok("Application has been verified for further processing.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}