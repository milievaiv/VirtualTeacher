using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO.CourseDTO;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Services;

namespace VirtualTeacherServicesTests
{
    [TestClass]
    public class CourseServiceTests
    {
        [TestMethod]
        public void Create_Creates_Course_Successfully()
        {
            // Arrange
            var createCourseDto = new CreateCourseDto
            {
                Title = "Test Course",
                CourseTopicId = 1,
                Description = "Test Description",
                StartDate = DateTime.Now
            };

            var teacher = new Teacher
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe"
            };

            var expectedCourse = new Course
            {
                Id = 1,
                Title = createCourseDto.Title,
                Creator = teacher,
                CourseTopic = new CourseTopic(), // Set the CourseTopic
                Description = createCourseDto.Description,
                StartDate = createCourseDto.StartDate
            };

            var mockCourseRepository = new Mock<ICourseRepository>();
            mockCourseRepository.Setup(repo => repo.Create(It.IsAny<Course>()))
                .Returns(expectedCourse);

            var mockCourseTopicRepository = new Mock<ICourseTopicRepository>();
            mockCourseTopicRepository.Setup(repo => repo.GetById(createCourseDto.CourseTopicId))
                .Returns(new CourseTopic { Id = createCourseDto.CourseTopicId });

            var mockTeacherRepository = new Mock<ITeacherRepository>();
            mockTeacherRepository.Setup(repo => repo.GetById(teacher.Id))
                .Returns(teacher);

            var courseService = new CourseService(mockCourseRepository.Object,
                                                  mockCourseTopicRepository.Object,
                                                  mockTeacherRepository.Object,
                                                  null);

            // Act
            var result = courseService.Create(createCourseDto, teacher);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedCourse.Title, result.Title);
            Assert.AreEqual(expectedCourse.Creator, result.Creator);
            Assert.AreEqual(expectedCourse.CourseTopic.Id, result.CourseTopic.Id); 
            Assert.AreEqual(expectedCourse.Description, result.Description);
            Assert.AreEqual(expectedCourse.StartDate, result.StartDate);
        }

    }
}
