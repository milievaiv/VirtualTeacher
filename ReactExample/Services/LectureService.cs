using ReactExample.Models;
using ReactExample.Repositories.Contracts;
using ReactExample.Services.Contracts;

namespace ReactExample.Services
{
    public class LectureService : ILectureService
    {
        #region State
        private readonly ILectureRepository lectureRepository;
        public LectureService(ILectureRepository lectureRepository)
        {
            this.lectureRepository = lectureRepository;
        }
        #endregion

        #region CRUD Methods
        public Lecture Create(Course course, Lecture lecture)
        {
            return lectureRepository.Create(course,lecture);
        }
        public IList<Lecture> GetAll()
        {
            return lectureRepository.GetAll();
        }

        public Lecture GetById(int id)
        {
            return lectureRepository.GetById(id);
        }

        public Lecture Update(Lecture lecture)
        {
            return lectureRepository.Update(lecture);
        }

        public bool Delete(Lecture lecture)
        {
            return lectureRepository.Delete(lecture);
        }
        #endregion

        #region Additional Methods
        public void AddAssignmentToLecture(int lectureId, Assignment newAssignment)
        {
            lectureRepository.AddAssignmentToLecture(lectureId, newAssignment);
        }
        #endregion
    }
}
