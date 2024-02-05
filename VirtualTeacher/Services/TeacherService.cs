using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.Repositories.Contracts;

namespace VirtualTeacher.Services
{
    public class TeacherService : ITeacherService
    {
        #region State
        private readonly IRegistrationService registrationService;
        private readonly ITeacherRepository teacherRepository;

        public TeacherService(
            IRegistrationService registrationService,
            ITeacherRepository teacherRepository)
        {
            this.registrationService = registrationService;
            this.teacherRepository = teacherRepository;
        }
        #endregion

        #region CRUD Methods
        
        public Teacher GetById(int id)
        {
            return teacherRepository.GetById(id);
        }

        public Teacher GetByEmail(string email)
        {
            return teacherRepository.GetByEmail(email);
        }

        public bool Delete(int id)
        {
            return teacherRepository.Delete(id);
        }
        #endregion

        #region Additional Methods
        public IList<ApprovedTeacher> GetApprovedTeachers()
        {
            return teacherRepository.GetApprovedTeachers();
        }

        public IList<Course> GetCoursesCreated(Teacher teacher)
        {
            return teacherRepository.GetCoursesCreated(teacher);
        }
        #endregion
    }
}
