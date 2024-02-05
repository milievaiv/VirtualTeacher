using Microsoft.AspNetCore.Cors.Infrastructure;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO;

namespace VirtualTeacher.Services.Contracts
{
    public interface ICourseService
    {
        Course Create(CreateCourseDto createCourseModel, Teacher teacher);
        IList<Course> GetAll();
        Course GetById(int id);
        IList<Course> GetByTitle(string courseName);
        void Update(int courseId, Course updatedCourse);
        bool Delete(Course course); 
        public void PublicizeCourse(int courseId);
        public void MarkAsDraft(int courseId);        
        void AddLectureToCourse(int courseId, Lecture newLecture);
    }
}
