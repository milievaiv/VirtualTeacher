namespace VirtualTeacher.Models.DTO.AuthenticationDTO
{
    public class PasswordHashInfo
    {
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
