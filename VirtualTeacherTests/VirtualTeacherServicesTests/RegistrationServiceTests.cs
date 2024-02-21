using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Models.DTO.AuthenticationDTO;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Services;

namespace VirtualTeacherServicesTests
{
    [TestClass]
    public class RegistrationServiceTests
    {
        [TestMethod]
        public void GeneratePasswordHashAndSalt_Throws_Exception_If_Email_Already_Exists()
        {
            // Arrange
            var registerModel = new RegisterDto { Email = "existing@example.com", Password = "password" };
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repo => repo.UserExists(registerModel.Email)).Returns(true);
            var registrationService = new RegistrationService(userRepositoryMock.Object);

            // Act & Assert
            Assert.ThrowsException<DuplicateEntityException>(() => registrationService.GeneratePasswordHashAndSalt(registerModel));
        }

        [TestMethod]
        public void GeneratePasswordHashAndSalt_Generates_Password_Hash_And_Salt_Successfully()
        {
            // Arrange
            var registerModel = new RegisterDto { Email = "new@example.com", Password = "password" };
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repo => repo.UserExists(registerModel.Email)).Returns(false);
            var registrationService = new RegistrationService(userRepositoryMock.Object);

            // Act
            var result = registrationService.GeneratePasswordHashAndSalt(registerModel);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.PasswordHash);
            Assert.IsNotNull(result.PasswordSalt);
            Assert.AreEqual(64, result.PasswordHash.Length); // Each byte of hash is represented by two characters in hexadecimal
            Assert.AreEqual(128, result.PasswordSalt.Length); // Each byte of salt is represented by two characters in hexadecimal
        }


    }
}
