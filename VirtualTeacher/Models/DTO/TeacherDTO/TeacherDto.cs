using VirtualTeacher.Models.DTO.UserDTO;

namespace VirtualTeacher.Models.DTO.TeacherDTO
{
    public class TeacherDto : UserDto
    {
        public bool Approved { get; set; }
    }
}
