using VirtualTeacher.Models.DTO;

namespace VirtualTeacher.Services.Contracts
{
    public interface IEmailService
    {
        void SendVerificationEmail(string email, TeacherCandidate contents);
        Task FindEmail(string requestId);
    }
}
