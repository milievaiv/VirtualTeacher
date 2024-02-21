using Microsoft.AspNetCore.Cors.Infrastructure;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO.CourseDTO;
using VirtualTeacher.Models.QueryParameters;

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
        void PublicizeCourse(int courseId);
        void MarkAsDraft(int courseId);        
        void AddLectureToCourse(int courseId, Lecture newLecture);
        // Course EnrollStudentInCourse(Student student, Course course);
        void EnrollStudentInCourse(int studentId, int courseId);
        void RateCourse(int courseId, int studentId, int ratingValue, string feedback);
        int GetAllCourseEnrollments();
        IList<Course> FilterBy(CourseQueryParameters filterParameters);
    }
}
