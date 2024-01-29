namespace VirtualTeacher.Models.DTO
{
    public class PasswordHashInfo
    {
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
