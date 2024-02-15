using VirtualTeacher.Models;

namespace VirtualTeacher.Repositories.Contracts

{
    public interface ICourseRepository
    {
        Course Create(Course course);
        IList<Course> GetAll();
        Course GetById(int id);
        IList<Course> GetByTitle(string courseTitle);
        void Update(int courseId, Course updatedCourse);
        bool Delete(Course course);
        public void PublicizeCourse(int courseId);
        public void MarkAsDraft(int courseId);            
        void AddLectureToCourse(int courseId, Lecture newLecture);
        void RateCourse(int courseId, int studentId, int ratingValue, string feedback);
    }
}
