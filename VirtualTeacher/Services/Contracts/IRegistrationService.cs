using VirtualTeacher.Models.DTO.AuthenticationDTO;

namespace VirtualTeacher.Services.Contracts
{
    public interface IRegistrationService
    {
        PasswordHashInfo GeneratePasswordHashAndSalt(RegisterDto registerModel);
    }
}
