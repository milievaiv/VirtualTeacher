﻿using ReactExample.Models.DTO;
using ReactExample.Models;
using ReactExample.Models.DTO.CourseDTO;
using ReactExample.Models.DTO.AssignmentDTO;

namespace ReactExample.Helpers.Contracts
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
