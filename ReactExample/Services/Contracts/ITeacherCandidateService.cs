using ReactExample.Models;
using ReactExample.Models.DTO;

namespace ReactExample.Services.Contracts
{
    public interface ITeacherCandidateService
    {
        string ProcessSubmission(TeacherCandidateDto teacherCandidateDto);

    }
}
