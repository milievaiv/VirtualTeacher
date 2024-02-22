using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO.AuthenticationDTO;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Services;

namespace VirtualTeacher.Tests.Services
{
    [TestClass]
    public class VerificationServiceTests
    {
        //[TestMethod]
        //public void AuthenticateUser_Returns_Admin_When_Admin_Exists()
        //{
        //    // Arrange
        //    var loginModel = new LoginDto { Email = "admin@example.com", Password = "testpassword" };
        //    var admin = new Admin { Email = loginModel.Email };
        //    var salt = new byte[64]; // Mock a salt value
        //    var adminsRepositoryMock = new Mock<IAdminRepository>();
        //    adminsRepositoryMock.Setup(x => x.GetAdminByEmail(loginModel.Email)).Returns(admin);
        //    var verificationService = new VerificationService(null, adminsRepositoryMock.Object, null, null);

        //    // Act
        //    var result = verificationService.AuthenticateUser(loginModel);

        //    // Assert
        //    Assert.AreEqual(admin, result);
        //}


        //[TestMethod]
        //public void AuthenticateUser_Returns_Student_When_Student_Exists()
        //{
        //    // Arrange
        //    var loginModel = new LoginDto { Email = "student@example.com", Password = "testpassword" };
        //    var student = new Student { Email = loginModel.Email };
        //    var studentRepositoryMock = new Mock<IStudentRepository>();
        //    studentRepositoryMock.Setup(x => x.GetByEmail(loginModel.Email)).Returns(student);
        //    var verificationService = new VerificationService(null, null, studentRepositoryMock.Object, null);

        //    // Act
        //    var result = verificationService.AuthenticateUser(loginModel);

        //    // Assert
        //    Assert.AreEqual(student, result);
        //}

        //[TestMethod]
        //public void AuthenticateUser_Returns_Teacher_When_Teacher_Exists()
        //{
        //    // Arrange
        //    var loginModel = new LoginDto { Email = "teacher@example.com", Password = "testpassword" };
        //    var teacher = new Teacher { Email = loginModel.Email };
        //    var teacherRepositoryMock = new Mock<ITeacherRepository>();
        //    teacherRepositoryMock.Setup(x => x.GetByEmail(loginModel.Email)).Returns(teacher);
        //    var verificationService = new VerificationService(null, null, null, teacherRepositoryMock.Object);

        //    // Act
        //    var result = verificationService.AuthenticateUser(loginModel);

        //    // Assert
        //    Assert.AreEqual(teacher, result);
        //}

        [TestMethod]
        public void AuthenticateUser_Throws_UnauthorizedOperationException_When_No_User_Found()
        {
            // Arrange
            var loginModel = new LoginDto { Email = "nonexistent@example.com", Password = "testpassword" };
            var adminsRepositoryMock = new Mock<IAdminRepository>();
            var studentRepositoryMock = new Mock<IStudentRepository>();
            var teacherRepositoryMock = new Mock<ITeacherRepository>();
            var verificationService = new VerificationService(null, adminsRepositoryMock.Object, studentRepositoryMock.Object, teacherRepositoryMock.Object);

            // Act & Assert
            Assert.ThrowsException<UnauthorizedOperationException>(() => verificationService.AuthenticateUser(loginModel));
        }

        // Additional test methods for AuthenticateAdmin, AuthenticateStudent, AuthenticateTeacher, and VerifyPasswordHash can be added similarly.
    }
}
