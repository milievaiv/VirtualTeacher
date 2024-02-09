using System.Net.Mail;
using System.Net;
using ReactExample.Models;
using ReactExample.Models.DTO;
using ReactExample.Services.Contracts;

namespace ReactExample.Services
{
    public class TeacherCandidateService : ITeacherCandidateService
    {
        private readonly Dictionary<string, (TeacherCandidateDto, string)> _temporaryStorage = new Dictionary<string, (TeacherCandidateDto, string)>();
        private readonly IEmailService _emailService;
        //private readonly IEmailReplyService _emailReplyService;

        public TeacherCandidateService(IEmailService emailService/*, IEmailReplyService emailReplyService*/)
        {
            _emailService = emailService;
            //_emailReplyService = emailReplyService;
        }
        public string ProcessSubmission(TeacherCandidateDto teacherCandidateDto)
        {
            if (string.IsNullOrEmpty(teacherCandidateDto.Email))
            {
                throw new ArgumentException("Email is required.");
            }

            var verificationCode = Guid.NewGuid().ToString().Substring(0, 6);

            _temporaryStorage[verificationCode] = (teacherCandidateDto, verificationCode);

            return verificationCode;
        }
    }
}
