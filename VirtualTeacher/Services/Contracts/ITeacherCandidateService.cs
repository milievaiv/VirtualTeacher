using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO.TeacherDTO;

namespace VirtualTeacher.Services.Contracts
{
    public interface ITeacherCandidateService
    {
        string ProcessSubmission(TeacherCandidateDto teacherCandidateDto);

    }
}
