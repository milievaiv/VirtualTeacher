using ReactExample.Helpers.Contracts;
using ReactExample.Models;
using ReactExample.Models.DTO;
using ReactExample.Models.DTO.AssignmentDTO;
using ReactExample.Models.DTO.CourseDTO;

namespace ReactExample.Helpers
{
    public class ModelMapper : IModelMapper
    {
        #region User Mapping
        public BaseUser MapToBaseUser(UserProfileUpdateDto model)
        {
            return new BaseUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName
            };
        }

        public IList<UserResponseDto> MapToUserResponseDtoList(IEnumerable<BaseUser> users)
        {
            return users.Select(MapToUserResponseDto).ToList();
        }

        public UserResponseDto MapToUserResponseDto(BaseUser user)
        {
            return new UserResponseDto
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
        #endregion

        #region Course Mapping
        public IList<CourseResponseDto> MapToCoursesResponseDto(IEnumerable<Course> courses)
        {
            return courses.Select(MapToCourseResponseDto).ToList();
        }

        public CourseResponseDto MapToCourseResponseDto(Course course)
        {
            return new CourseResponseDto
            {
                Id = course.Id,
                Title = course.Title,
                Topic = MapToCourseTopicDto(course.CourseTopic), 
                Description = course.Description,
                StartDate = course.StartDate,
                Creator = course.Creator.FirstName,
            };
        }
        public Course MapToUpdateCourse(UpdateCourseDto updateCourseDto)
        {
            return new Course
            {
                Title = updateCourseDto.Title,
                Description = updateCourseDto.Description,
                CourseTopic = MapToCourseTopic(updateCourseDto.CourseTopic),
                StartDate = updateCourseDto.StartDate,
            };
        }

        public CourseTopic MapToCourseTopic(string topic)
        { 
            return new CourseTopic { Topic = topic };
        }

        public CourseTopicDto MapToCourseTopicDto(CourseTopic courseTopic)
        {
            return new CourseTopicDto
            {
                Topic = courseTopic?.Topic
            };
        }
        #endregion

        public Assignment MapToAssignment(AssignmentDto assignmentDto)
        {
            return new Assignment
            {
                Content = assignmentDto.Content
            };
        }

        public Lecture MapToLectue(LectureDto lectureDto)
        {
            return new Lecture
            { 
                Title = lectureDto.Title,
                Description = lectureDto.Description,
                VideoURL = lectureDto.VideoURL
            };
        }
    }
}
