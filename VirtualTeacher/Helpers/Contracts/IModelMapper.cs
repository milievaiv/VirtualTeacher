using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO.LectureDTO;
using VirtualTeacher.Models.DTO.UserDTO;
using VirtualTeacher.Models.DTO.CourseDTO;
using VirtualTeacher.Models.DTO.AssignmentDTO;

namespace VirtualTeacher.Helpers.Contracts
{
    public interface IModelMapper
    {
        BaseUser MapToBaseUser(UserProfileUpdateDto model);
        public IList<UserResponseDto> MapToUserResponseDtoList(IEnumerable<BaseUser> users);
        UserResponseDto MapToUserResponseDto(BaseUser user);
        public IList<CourseResponseDto> MapToCoursesResponseDto(IEnumerable<Course> courses);
        public CourseResponseDto MapToCourseResponseDto(Course course);
        Course MapToUpdateCourse(UpdateCourseDto updateCourseDto);
        public CourseTopicDto MapToCourseTopicDto(CourseTopic courseTopic);
        CourseTopic MapToCourseTopic(string topic);
        Assignment MapToAssignment(AssignmentDto assignmentDto);
        Lecture MapToLectue(LectureDto lectureDto);
    }
}
