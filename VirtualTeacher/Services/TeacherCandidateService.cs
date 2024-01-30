using System.Net.Mail;
using System.Net;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Services
{
    public class TeacherCandidateService : ITeacherCandidateService
    {
        private readonly Dictionary<string, (TeacherCandidate, string)> _temporaryStorage = new Dictionary<string, (TeacherCandidate, string)>();
        private readonly IEmailService _emailService;
        //private readonly IEmailReplyService _emailReplyService;

        public TeacherCandidateService(IEmailService emailService/*, IEmailReplyService emailReplyService*/)
        {
            _emailService = emailService;
            //_emailReplyService = emailReplyService;
        }
        public string ProcessSubmission(TeacherCandidate teacherCandidateDto)
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
