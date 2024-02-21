using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO.AuthenticationDTO;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Services;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacherServicesTests
{
    [TestClass]
    public class AdminServiceTests
    {
        [TestMethod]
        public void Register_Returns_Admin_When_Successful()
        {
            // Arrange
            var registerDto = new RegisterDto
            {
                Email = "test@example.com",
                Password = "password",
                FirstName = "John",
                LastName = "Doe"
            };

            var passInfo = new PasswordHashInfo
            {
                PasswordHash = new byte[] { 1, 2, 3 },
                PasswordSalt = new byte[] { 4, 5, 6 }
            };

            var expectedAdmin = new Admin
            {
                Email = registerDto.Email,
                PasswordHash = passInfo.PasswordHash,
                PasswordSalt = passInfo.PasswordSalt,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Role = UserRole.Administrator
            };

            var mockRegistrationService = new Mock<IRegistrationService>();
            mockRegistrationService.Setup(service => service.GeneratePasswordHashAndSalt(registerDto))
                .Returns(passInfo);

            var mockAdminRepository = new Mock<IAdminRepository>();
            mockAdminRepository.Setup(repo => repo.Create(It.IsAny<Admin>()))
                .Returns(expectedAdmin);

            var adminService = new AdminService(null, mockAdminRepository.Object, mockRegistrationService.Object);

            // Act
            var result = adminService.Register(registerDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedAdmin.Email, result.Email);
            Assert.AreEqual(expectedAdmin.FirstName, result.FirstName);
            Assert.AreEqual(expectedAdmin.LastName, result.LastName);
            Assert.AreEqual(expectedAdmin.Role, result.Role);
        }
        [TestMethod]
        public void GetAll_Returns_All_Admins()
        {
            // Arrange
            var expectedAdmins = new List<Admin>
            {
                new Admin { Id = 1, Email = "admin1@example.com", FirstName = "Admin1" },
                new Admin { Id = 2, Email = "admin2@example.com", FirstName = "Admin2" },
                new Admin { Id = 3, Email = "admin3@example.com", FirstName = "Admin3" }
            };

            var mockAdminRepository = new Mock<IAdminRepository>();
            mockAdminRepository.Setup(repo => repo.GetAll())
                .Returns(expectedAdmins);

            var adminService = new AdminService(null, mockAdminRepository.Object, null);

            // Act
            var result = adminService.GetAll();

            // Assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(expectedAdmins.ToArray(), result.ToArray());
        }

        [TestMethod]
        public void GetByEmail_Returns_Admin_With_Specified_Email()
        {
            // Arrange
            var email = "admin@example.com";
            var expectedAdmin = new Admin
            {
                Id = 1,
                Email = email,
                FirstName = "Admin",
                LastName = "Doe"
            };

            var mockAdminRepository = new Mock<IAdminRepository>();
            mockAdminRepository.Setup(repo => repo.GetAdminByEmail(email))
                .Returns(expectedAdmin);

            var adminService = new AdminService(null, mockAdminRepository.Object, null);

            // Act
            var result = adminService.GetByEmail(email);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedAdmin, result);
        }

        [TestMethod]
        public void ApproveTeacher_Returns_Approved_Teacher()
        {
            // Arrange
            var email = "teacher@example.com";
            var expectedTeacher = new ApprovedTeacher
            {
                Id = 1,
                Email = email
            };

            var mockAdminRepository = new Mock<IAdminRepository>();
            mockAdminRepository.Setup(repo => repo.ApproveTeacher(email))
                .Returns(expectedTeacher);

            var adminService = new AdminService(null, mockAdminRepository.Object, null);

            // Act
            var result = adminService.ApproveTeacher(email);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedTeacher.Id, result.Id);
            Assert.AreEqual(expectedTeacher.Email, result.Email);
        }
    }
}
