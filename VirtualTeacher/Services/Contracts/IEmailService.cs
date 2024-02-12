using VirtualTeacher.Models.DTO.TeacherDTO;

namespace VirtualTeacher.Services.Contracts
{
    public interface IEmailService
    {
        void SendVerificationEmail(string email, TeacherCandidateDto contents);
        Task VerifyApplication(string requestId);
    }
}
