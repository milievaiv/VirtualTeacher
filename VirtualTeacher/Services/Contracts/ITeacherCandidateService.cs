using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO;

namespace VirtualTeacher.Services.Contracts
{
    public interface ITeacherCandidateService
    {
        string ProcessSubmission(TeacherCandidateDto teacherCandidateDto);

    }
}
