using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using VirtualTeacher.Models;
using VirtualTeacher.Repositories.Contracts;

namespace VirtualTeacher.Services.Tests
{
    [TestClass]
    public class StudentServiceTests
    {
        [TestMethod]
        public void GetAll_Returns_All_Students_Successfully()
        {
            // Arrange
            var expectedStudents = new List<Student>
    {
        new Student { Id = 1 },
        new Student { Id = 2 },
        new Student { Id = 3 }
    };

            var studentRepositoryMock = new Mock<IStudentRepository>();
            studentRepositoryMock.Setup(repo => repo.GetAll()).Returns(expectedStudents);

            var studentService = new StudentService(studentRepositoryMock.Object);

            // Act
            var result = studentService.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedStudents.Count, result.Count);
            CollectionAssert.AreEqual(expectedStudents, result.ToList());
        }


        [TestMethod]
        public void GetById_Returns_Correct_Student_Successfully()
        {
            // Arrange
            var expectedStudent = new Student { Id = 1 };

            var studentRepositoryMock = new Mock<IStudentRepository>();
            studentRepositoryMock.Setup(repo => repo.GetById(expectedStudent.Id)).Returns(expectedStudent);

            var studentService = new StudentService(studentRepositoryMock.Object);

            // Act
            var result = studentService.GetById(expectedStudent.Id);

            // Assert
            Assert.AreEqual(expectedStudent, result);
        }

        [TestMethod]
        public void GetByEmail_Returns_Correct_Student_Successfully()
        {
            // Arrange
            var email = "test@example.com";
            var expectedStudent = new Student { Id = 1, Email = email };

            var studentRepositoryMock = new Mock<IStudentRepository>();
            studentRepositoryMock.Setup(repo => repo.GetByEmail(email)).Returns(expectedStudent);

            var studentService = new StudentService(studentRepositoryMock.Object);

            // Act
            var result = studentService.GetByEmail(email);

            // Assert
            Assert.AreEqual(expectedStudent, result);
        }

        [TestMethod]
        public void Update_Updates_Student_Successfully()
        {
            // Arrange
            var studentToUpdate = new Student { Id = 1 };

            var studentRepositoryMock = new Mock<IStudentRepository>();
            studentRepositoryMock.Setup(repo => repo.Update(studentToUpdate)).Returns(studentToUpdate);

            var studentService = new StudentService(studentRepositoryMock.Object);

            // Act
            var result = studentService.Update(studentToUpdate);

            // Assert
            Assert.AreEqual(studentToUpdate, result);
        }

        [TestMethod]
        public void Delete_Deletes_Student_Successfully()
        {
            // Arrange
            var studentId = 1;

            var studentRepositoryMock = new Mock<IStudentRepository>();
            studentRepositoryMock.Setup(repo => repo.Delete(studentId)).Returns(true);

            var studentService = new StudentService(studentRepositoryMock.Object);

            // Act
            var result = studentService.Delete(studentId);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetEnrolledCourses_Returns_Enrolled_Courses_Successfully()
        {
            // Arrange
            var student = new Student { Id = 1 };
            var expectedCourses = new List<Course>
    {
        new Course { Id = 1, Title = "Course 1" },
        new Course { Id = 2, Title = "Course 2" }
    };

            var studentRepositoryMock = new Mock<IStudentRepository>();
            studentRepositoryMock.Setup(repo => repo.GetEnrolledCourses(student)).Returns(expectedCourses);

            var studentService = new StudentService(studentRepositoryMock.Object);

            // Act
            var result = studentService.GetEnrolledCourses(student);

            // Assert
            CollectionAssert.AreEqual(expectedCourses, result.ToList());
        }

        [TestMethod]
        public void GetCompletedCourses_Returns_Completed_Courses_Successfully()
        {
            // Arrange
            var student = new Student { Id = 1 };
            var expectedCourses = new List<Course>
    {
        new Course { Id = 1, Title = "Course 1" },
        new Course { Id = 2, Title = "Course 2" }
    };

            var studentRepositoryMock = new Mock<IStudentRepository>();
            studentRepositoryMock.Setup(repo => repo.GetCompletedCourses(student)).Returns(expectedCourses);

            var studentService = new StudentService(studentRepositoryMock.Object);

            // Act
            var result = studentService.GetCompletedCourses(student);

            // Assert
            CollectionAssert.AreEqual(expectedCourses, result.ToList());
        }

    }
}
