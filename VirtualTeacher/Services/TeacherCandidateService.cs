using System.Text;
using VirtualTeacher.Models.DTO.TeacherDTO;
using VirtualTeacher.Models;
using VirtualTeacher.Services.Contracts;
using System.Security.Cryptography;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Exceptions;


namespace VirtualTeacher.Services
{
    public class TeacherCandidateService : ITeacherCandidateService
    {
        private readonly Dictionary<string, (TeacherCandidateDto, string)> _temporaryStorage = new Dictionary<string, (TeacherCandidateDto, string)>();
        private readonly IEmailService _emailService;
        private readonly ITeacherRepository _teacherRepository;
        //private readonly IEmailReplyService _emailReplyService;

        public TeacherCandidateService(IEmailService emailService, ITeacherRepository teacherRepository/*, IEmailReplyService emailReplyService*/)
        {
            _emailService = emailService;
            _teacherRepository = teacherRepository;
            //_emailReplyService = emailReplyService;
        }

        public bool FiveDaysPastApplication(string email)
        {
            return _teacherRepository.FiveDaysPastApplication(email);
        }
        public string ProcessSubmission(TeacherCandidateDto teacherCandidateDto)
        {
            if (string.IsNullOrEmpty(teacherCandidateDto.Email))
            {
                throw new ArgumentException("Email is required.");
            }

            //var hashCode = teacherCandidateDto.Email.GetHashCode();

            // Convert the hash code to a 6-character string
            var verificationCode = GenerateVerificationCode(teacherCandidateDto.Email);


            _temporaryStorage[verificationCode] = (teacherCandidateDto, verificationCode);

            return verificationCode;
        }

        private string GenerateVerificationCode(string email)
        {
            // Concatenate email and current date/time
            var inputString = $"{email}{DateTime.Now}";

            // Compute hash value of the concatenated string
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(inputString));

                // Convert the byte array to a hexadecimal string
                StringBuilder stringBuilder = new StringBuilder();
                foreach (byte b in data)
                {
                    stringBuilder.Append(b.ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }
    }
}
