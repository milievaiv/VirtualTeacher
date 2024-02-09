using ReactExample.Models.DTO;

namespace ReactExample.Services.Contracts
{
    public interface IEmailService
    {
        void SendVerificationEmail(string email, TeacherCandidateDto contents);
        Task VerifyApplication(string requestId);
    }
}
