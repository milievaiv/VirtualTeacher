using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using VirtualTeacher.Constants;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO.AuthenticationDTO;
using VirtualTeacher.Models.QueryParameters;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Services;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Tests.Services
{
    [TestClass]
    public class UserServiceTests
    {
        [TestMethod]
        public void GetAll_Returns_All_Users()
        {
            // Arrange
            var expectedUsers = new List<BaseUser>
            {
                new Teacher { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" },
                new Student { Id = 2, FirstName = "Alice", LastName = "Smith", Email = "alice.smith@example.com" }
            };

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.GetAll()).Returns(expectedUsers);

            var userService = new UserService(userRepositoryMock.Object, null, null, null, null);

            // Act
            var actualUsers = userService.GetAll();

            // Assert
            CollectionAssert.AreEqual(expectedUsers, actualUsers.ToList());
        }

        [TestMethod]
        public void GetById_Returns_Correct_User()
        {
            // Arrange
            int userId = 1;
            var expectedUser = new Teacher { Id = userId, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.GetById(userId)).Returns(expectedUser);

            var userService = new UserService(userRepositoryMock.Object, null, null, null, null);

            // Act
            var actualUser = userService.GetById(userId);

            // Assert
            Assert.AreEqual(expectedUser, actualUser);
        }

        [TestMethod]
        public void GetByFirstName_Returns_User_When_Found()
        {
            // Arrange
            string firstName = "John";
            var user = new BaseUser { FirstName = firstName };
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.GetByFirstName(firstName)).Returns(user);
            var userService = new UserService(userRepositoryMock.Object, null, null, null, null);

            // Act
            var result = userService.GetByFirstName(firstName);

            // Assert
            Assert.AreEqual(user, result);
        }

        [TestMethod]
        public void GetByLastName_Returns_User_When_Found()
        {
            // Arrange
            string lastName = "Doe";
            var user = new BaseUser { LastName = lastName };
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.GetByLastName(lastName)).Returns(user);
            var userService = new UserService(userRepositoryMock.Object, null, null, null, null);

            // Act
            var result = userService.GetByLastName(lastName);

            // Assert
            Assert.AreEqual(user, result);
        }

        [TestMethod]
        public void GetByEmail_Returns_User_When_Found()
        {
            // Arrange
            string email = "test@example.com";
            var user = new BaseUser { Email = email };
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.GetByEmail(email)).Returns(user);
            var userService = new UserService(userRepositoryMock.Object, null, null, null, null);

            // Act
            var result = userService.GetByEmail(email);

            // Assert
            Assert.AreEqual(user, result);
        }

        [TestMethod]
        public void FilterBy_Returns_List_Of_Users()
        {
            // Arrange
            var userQueryParameters = new UserQueryParameters();
            var users = new List<BaseUser> { new BaseUser(), new BaseUser() };
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.FilterBy(userQueryParameters)).Returns(users);
            var userService = new UserService(userRepositoryMock.Object, null, null, null, null);

            // Act
            var result = userService.FilterBy(userQueryParameters);

            // Assert
            Assert.AreEqual(users, result);
        }

        [TestMethod]
        public void Update_Returns_Updated_User()
        {
            // Arrange
            int userId = 1;
            var updatedUser = new BaseUser { Id = userId };
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.Update(userId, updatedUser)).Returns(updatedUser);
            var userService = new UserService(userRepositoryMock.Object, null, null, null, null);

            // Act
            var result = userService.Update(userId, updatedUser);

            // Assert
            Assert.AreEqual(updatedUser, result);
        }

        //[TestMethod]
        //public void Register_Returns_Student_When_Email_Not_Exists_In_Approved_Teachers()
        //{
        //    // Arrange
        //    var registerDto = new RegisterDto
        //    {
        //        Email = "test@example.com",
        //        FirstName = "John",
        //        LastName = "Doe",
        //        Password = "testpassword"
        //    };

        //    var passInfo = new PasswordHashInfo
        //    {
        //        PasswordHash = new byte[] { 0x12, 0x34, 0x56 },
        //        PasswordSalt = new byte[] { 0x78, 0x9A, 0xBC }
        //    };

        //    var approvedTeachers = new List<ApprovedTeacher>();
        //    var teacherRepositoryMock = new Mock<ITeacherRepository>();
        //    teacherRepositoryMock.Setup(x => x.GetApprovedTeachers()).Returns(approvedTeachers);

        //    var userRepositoryMock = new Mock<IUserRepository>();

        //    var registrationServiceMock = new Mock<IRegistrationService>();
        //    registrationServiceMock.Setup(x => x.GeneratePasswordHashAndSalt(registerDto)).Returns(passInfo);

        //    // Pass the mock registration service to the UserService constructor
        //    var userService = new UserService(userRepositoryMock.Object, registrationServiceMock.Object, null, null, teacherRepositoryMock.Object);

        //    // Act
        //    var result = userService.Register(registerDto);

        //    // Assert
        //    Assert.IsInstanceOfType(result, typeof(Student));
        //}






        [TestMethod]
        public void ChangePassword_Throws_Exception_When_User_Not_Found()
        {
            // Arrange
            int userId = 1;
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.GetById(userId)).Returns<BaseUser>(null);

            var userService = new UserService(userRepositoryMock.Object, null, null, null, null);

            // Act & Assert
            Assert.ThrowsException<EntityNotFoundException>(() => userService.ChangePassword(userId, "oldPassword", "newPassword"));
        }

        // Add more test methods as needed
    }
}
