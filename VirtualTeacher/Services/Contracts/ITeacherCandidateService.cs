using VirtualTeacher.Models.DTO.TeacherDTO;
using VirtualTeacher.Models;

namespace VirtualTeacher.Services.Contracts
{
    public interface ITeacherCandidateService
    {
        string ProcessSubmission(TeacherCandidateDto teacherCandidateDto);
        bool FiveDaysPastApplication(string email);
    }
}
