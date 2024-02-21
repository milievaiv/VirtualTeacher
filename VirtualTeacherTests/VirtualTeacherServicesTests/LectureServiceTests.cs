using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using VirtualTeacher.Models;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Services;

namespace VirtualTeacherServicesTests
{
    [TestClass]
    public class LectureServiceTests
    {
        [TestMethod]
        public void Create_Creates_Lecture_Successfully()
        {
            // Arrange
            var course = new Course { Id = 1, Title = "Test Course" };
            var lecture = new Lecture { Id = 1, Title = "Test Lecture" };

            var mockLectureRepository = new Mock<ILectureRepository>();
            mockLectureRepository.Setup(repo => repo.Create(course, lecture)).Returns(lecture);

            var lectureService = new LectureService(mockLectureRepository.Object);

            // Act
            var result = lectureService.Create(course, lecture);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(lecture, result);
        }

        [TestMethod]
        public void GetAll_Returns_All_Lectures()
        {
            // Arrange
            var lectures = new List<Lecture>
            {
                new Lecture { Id = 1, Title = "Lecture 1" },
                new Lecture { Id = 2, Title = "Lecture 2" },
                new Lecture { Id = 3, Title = "Lecture 3" }
            };

            var mockLectureRepository = new Mock<ILectureRepository>();
            mockLectureRepository.Setup(repo => repo.GetAll()).Returns(lectures);

            var lectureService = new LectureService(mockLectureRepository.Object);

            // Act
            var result = lectureService.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(lectures.Count, result.Count);
        }

        [TestMethod]
        public void GetById_Returns_Correct_Lecture()
        {
            // Arrange
            var lectureId = 1;
            var expectedLecture = new Lecture { Id = lectureId, Title = "Test Lecture" };

            var mockLectureRepository = new Mock<ILectureRepository>();
            mockLectureRepository.Setup(repo => repo.GetById(lectureId)).Returns(expectedLecture);

            var lectureService = new LectureService(mockLectureRepository.Object);

            // Act
            var result = lectureService.GetById(lectureId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedLecture, result);
        }

        [TestMethod]
        public void Update_Updates_Lecture_Successfully()
        {
            // Arrange
            var lectureToUpdate = new Lecture { Id = 1, Title = "Test Lecture", Description = "Initial description" };
            var updatedLecture = new Lecture { Id = 1, Title = "Test Lecture", Description = "Updated description" };

            var mockLectureRepository = new Mock<ILectureRepository>();
            mockLectureRepository.Setup(repo => repo.Update(lectureToUpdate)).Returns(updatedLecture);

            var lectureService = new LectureService(mockLectureRepository.Object);

            // Act
            var result = lectureService.Update(lectureToUpdate);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updatedLecture, result);
        }

        [TestMethod]
        public void Delete_Deletes_Lecture_Successfully()
        {
            // Arrange
            var lectureToDelete = new Lecture { Id = 1, Title = "Test Lecture" };

            var mockLectureRepository = new Mock<ILectureRepository>();
            mockLectureRepository.Setup(repo => repo.Delete(lectureToDelete)).Returns(true);

            var lectureService = new LectureService(mockLectureRepository.Object);

            // Act
            var result = lectureService.Delete(lectureToDelete);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AddAssignmentToLecture_Adds_Assignment_Successfully()
        {
            // Arrange
            var lectureId = 1;
            var newAssignment = new Assignment { Id = 1 };

            var mockLectureRepository = new Mock<ILectureRepository>();

            var lectureService = new LectureService(mockLectureRepository.Object);

            // Act
            lectureService.AddAssignmentToLecture(lectureId, newAssignment);

            // Assert
            mockLectureRepository.Verify(repo => repo.AddAssignmentToLecture(lectureId, newAssignment), Times.Once);
        }
    }
}
