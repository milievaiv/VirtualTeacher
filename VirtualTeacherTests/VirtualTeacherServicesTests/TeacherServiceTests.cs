using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using VirtualTeacher.Models;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Services.Tests
{
    [TestClass]
    public class TeacherServiceTests
    {
        [TestMethod]
        public void GetById_Returns_Correct_Teacher_Successfully()
        {
            // Arrange
            int teacherId = 1;
            var expectedTeacher = new Teacher { Id = teacherId };

            var teacherRepositoryMock = new Mock<ITeacherRepository>();
            teacherRepositoryMock.Setup(repo => repo.GetById(teacherId)).Returns(expectedTeacher);

            var registrationServiceMock = new Mock<IRegistrationService>();

            var teacherService = new TeacherService(registrationServiceMock.Object, teacherRepositoryMock.Object);

            // Act
            var result = teacherService.GetById(teacherId);

            // Assert
            Assert.AreEqual(expectedTeacher, result);
        }

        [TestMethod]
        public void GetByEmail_Returns_Correct_Teacher_Successfully()
        {
            // Arrange
            string email = "test@example.com";
            var expectedTeacher = new Teacher { Email = email };

            var teacherRepositoryMock = new Mock<ITeacherRepository>();
            teacherRepositoryMock.Setup(repo => repo.GetByEmail(email)).Returns(expectedTeacher);

            var registrationServiceMock = new Mock<IRegistrationService>();

            var teacherService = new TeacherService(registrationServiceMock.Object, teacherRepositoryMock.Object);

            // Act
            var result = teacherService.GetByEmail(email);

            // Assert
            Assert.AreEqual(expectedTeacher, result);
        }

        [TestMethod]
        public void Delete_Deletes_Teacher_Successfully()
        {
            // Arrange
            int teacherId = 1;

            var teacherRepositoryMock = new Mock<ITeacherRepository>();
            teacherRepositoryMock.Setup(repo => repo.Delete(teacherId)).Returns(true);

            var registrationServiceMock = new Mock<IRegistrationService>();

            var teacherService = new TeacherService(registrationServiceMock.Object, teacherRepositoryMock.Object);

            // Act
            var result = teacherService.Delete(teacherId);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetApprovedTeachers_Returns_Approved_Teachers_Successfully()
        {
            // Arrange
            var expectedTeachers = new List<ApprovedTeacher>
    {
        new ApprovedTeacher { Id = 1 },
        new ApprovedTeacher { Id = 2 }
    };

            var teacherRepositoryMock = new Mock<ITeacherRepository>();
            teacherRepositoryMock.Setup(repo => repo.GetApprovedTeachers()).Returns(expectedTeachers);

            var registrationServiceMock = new Mock<IRegistrationService>();

            var teacherService = new TeacherService(registrationServiceMock.Object, teacherRepositoryMock.Object);

            // Act
            var result = teacherService.GetApprovedTeachers();

            // Assert
            CollectionAssert.AreEqual(expectedTeachers, result.ToList());
        }


        [TestMethod]
        public void GetCoursesCreated_Returns_Courses_Created_By_Teacher_Successfully()
        {
            // Arrange
            var teacher = new Teacher { Id = 1 };
            var expectedCourses = new List<Course>
    {
        new Course { Id = 1, Title = "Course 1" },
        new Course { Id = 2, Title = "Course 2" }
    };

            var teacherRepositoryMock = new Mock<ITeacherRepository>();
            teacherRepositoryMock.Setup(repo => repo.GetCoursesCreated(teacher)).Returns(expectedCourses);

            var registrationServiceMock = new Mock<IRegistrationService>();

            var teacherService = new TeacherService(registrationServiceMock.Object, teacherRepositoryMock.Object);

            // Act
            var result = teacherService.GetCoursesCreated(teacher);

            // Assert
            CollectionAssert.AreEqual(expectedCourses, result.ToList());
        }
    }
}
