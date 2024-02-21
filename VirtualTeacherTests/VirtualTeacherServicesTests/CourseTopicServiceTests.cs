using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using VirtualTeacher.Models;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Services;

namespace VirtualTeacherServicesTests
{
    [TestClass]
    public class CourseTopicServiceTests
    {
        [TestMethod]
        public void Create_Creates_CourseTopic_Successfully()
        {
            // Arrange
            var courseTopic = new CourseTopic { Id = 1, Topic = "Test Topic" };
            var expectedCourseTopic = new CourseTopic { Id = 1, Topic = "Test Topic" };

            var mockCourseTopicRepository = new Mock<ICourseTopicRepository>();
            mockCourseTopicRepository.Setup(repo => repo.Create(courseTopic))
                .Returns(expectedCourseTopic);

            var courseTopicService = new CourseTopicService(mockCourseTopicRepository.Object);

            // Act
            var result = courseTopicService.Create(courseTopic);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedCourseTopic.Id, result.Id);
            Assert.AreEqual(expectedCourseTopic.Topic, result.Topic);
        }

        [TestMethod]
        public void GetAll_Returns_All_CourseTopics()
        {
            // Arrange
            var expectedCourseTopics = new List<CourseTopic>
    {
        new CourseTopic { Id = 1, Topic = "Topic 1" },
        new CourseTopic { Id = 2, Topic = "Topic 2" },
        new CourseTopic { Id = 3, Topic = "Topic 3" }
    };

            var mockCourseTopicRepository = new Mock<ICourseTopicRepository>();
            mockCourseTopicRepository.Setup(repo => repo.GetAll())
                .Returns(expectedCourseTopics);

            var courseTopicService = new CourseTopicService(mockCourseTopicRepository.Object);

            // Act
            var result = courseTopicService.GetAll();

            // Assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(expectedCourseTopics.ToArray(), result.ToArray());
        }


        [TestMethod]
        public void GetById_Returns_CourseTopic_With_Specified_Id()
        {
            // Arrange
            var id = 1;
            var expectedCourseTopic = new CourseTopic { Id = 1, Topic = "Test Topic" };

            var mockCourseTopicRepository = new Mock<ICourseTopicRepository>();
            mockCourseTopicRepository.Setup(repo => repo.GetById(id))
                .Returns(expectedCourseTopic);

            var courseTopicService = new CourseTopicService(mockCourseTopicRepository.Object);

            // Act
            var result = courseTopicService.GetById(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedCourseTopic.Id, result.Id);
            Assert.AreEqual(expectedCourseTopic.Topic, result.Topic);
        }

        [TestMethod]
        public void Delete_Deletes_CourseTopic_Successfully()
        {
            // Arrange
            var id = 1;

            var mockCourseTopicRepository = new Mock<ICourseTopicRepository>();
            mockCourseTopicRepository.Setup(repo => repo.Delete(id))
                .Returns(true);

            var courseTopicService = new CourseTopicService(mockCourseTopicRepository.Object);

            // Act
            var result = courseTopicService.Delete(id);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
