using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using VirtualTeacher.Models.DTO.TeacherDTO;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Services.Tests
{
    [TestClass]
    public class TeacherCandidateServiceTests
    {
        [TestMethod]
        public void FiveDaysPastApplication_Returns_True_If_Five_Days_Past()
        {
            // Arrange
            var email = "test@example.com";

            var teacherRepositoryMock = new Mock<ITeacherRepository>();
            teacherRepositoryMock.Setup(repo => repo.FiveDaysPastApplication(email)).Returns(true);

            var teacherCandidateService = new TeacherCandidateService(null, teacherRepositoryMock.Object);

            // Act
            var result = teacherCandidateService.FiveDaysPastApplication(email);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ProcessSubmission_Returns_Verification_Code_Successfully()
        {
            // Arrange
            var email = "test@example.com";
            var teacherCandidateDto = new TeacherCandidateDto { Email = email };

            var emailServiceMock = new Mock<IEmailService>();
            var teacherRepositoryMock = new Mock<ITeacherRepository>();

            var teacherCandidateService = new TeacherCandidateService(emailServiceMock.Object, teacherRepositoryMock.Object);

            // Act
            var result = teacherCandidateService.ProcessSubmission(teacherCandidateDto);

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(result));
            Assert.AreEqual(64, result.Length); // SHA256 hash length
        }


        [TestMethod]
        public void ProcessSubmission_Throws_Exception_If_Email_Is_Null_Or_Empty()
        {
            // Arrange
            var teacherCandidateDto = new TeacherCandidateDto { Email = null };

            var emailServiceMock = new Mock<IEmailService>();
            var teacherRepositoryMock = new Mock<ITeacherRepository>();

            var teacherCandidateService = new TeacherCandidateService(emailServiceMock.Object, teacherRepositoryMock.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => teacherCandidateService.ProcessSubmission(teacherCandidateDto));

            // Arrange
            teacherCandidateDto.Email = "";

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => teacherCandidateService.ProcessSubmission(teacherCandidateDto));
        }
    }
}
