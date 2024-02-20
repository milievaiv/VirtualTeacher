//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using System.Collections.Generic;
//using VirtualTeacher.Models;
//using VirtualTeacher.Models.DTO.AuthenticationDTO;
//using VirtualTeacher.Models.QueryParameters;
//using VirtualTeacher.Repositories.Contracts;
//using VirtualTeacher.Services;
//using VirtualTeacher.Services.Contracts;

//namespace VirtualTeacher.Tests.Services
//{
//    [TestClass]
//    public class UserServiceTests
//    {
//        [TestMethod]
//        public void Register_Returns_Teacher_When_Email_Exists_In_Approved_Teachers()
//        {
//            // Arrange
//            var registerDto = new RegisterDto
//            {
//                Email = "test@example.com",
//                FirstName = "John",
//                LastName = "Doe",
//                Password = "testpassword"
//            };

//            var passInfo = new PasswordHashInfo
//            {
//                PasswordHash = new byte[] { 0x12, 0x34, 0x56 },
//                PasswordSalt = new byte[] { 0x78, 0x9A, 0xBC }
//            };

//            var approvedTeachers = new List<ApprovedTeacher> { new ApprovedTeacher { Email = registerDto.Email } };
//            var teacherRepositoryMock = new Mock<ITeacherRepository>();
//            teacherRepositoryMock.Setup(x => x.GetApprovedTeachers()).Returns(approvedTeachers);

//            var userRepositoryMock = new Mock<IUserRepository>();
//            userRepositoryMock.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<BaseUser>())).Returns<BaseUser>(x => x);

//            var registrationServiceMock = new Mock<IRegistrationService>();
//            registrationServiceMock.Setup(x => x.GeneratePasswordHashAndSalt(registerDto)).Returns(passInfo);

//            var userService = new UserService(userRepositoryMock.Object, registrationServiceMock.Object, null, null, teacherRepositoryMock.Object);

//            // Act
//            var result = userService.Register(registerDto);

//            // Assert
//            Assert.IsInstanceOfType(result, typeof(Teacher));
//            userRepositoryMock.Verify(x => x.Update(It.IsAny<int>(), It.IsAny<BaseUser>()), Times.Once);
//        }

//        [TestMethod]
//        public void Register_Returns_Student_When_Email_Not_Exists_In_Approved_Teachers()
//        {
//            // Arrange
//            var registerDto = new RegisterDto
//            {
//                Email = "test@example.com",
//                FirstName = "John",
//                LastName = "Doe",
//                Password = "testpassword"
//            };

//            var passInfo = new PasswordHashInfo
//            {
//                PasswordHash = new byte[] { 0x12, 0x34, 0x56 },
//                PasswordSalt = new byte[] { 0x78, 0x9A, 0xBC }
//            };

//            var approvedTeachers = new List<ApprovedTeacher>();
//            var teacherRepositoryMock = new Mock<ITeacherRepository>();
//            teacherRepositoryMock.Setup(x => x.GetApprovedTeachers()).Returns(approvedTeachers);

//            var studentRepositoryMock = new Mock<IStudentRepository>(); // Assuming there's an IStudentRepository
//            studentRepositoryMock.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<BaseUser>())).Returns<BaseUser>(x => x);

//            var userRepositoryMock = new Mock<IUserRepository>();
//            userRepositoryMock.Setup(x => x.Create(It.IsAny<BaseUser>())).Returns<BaseUser>(x => x);

//            var registrationServiceMock = new Mock<IRegistrationService>();
//            registrationServiceMock.Setup(x => x.GeneratePasswordHashAndSalt(registerDto)).Returns(passInfo);

//            var userService = new UserService(userRepositoryMock.Object, registrationServiceMock.Object, null, studentRepositoryMock.Object, teacherRepositoryMock.Object);

//            // Act
//            var result = userService.Register(registerDto);

//            // Assert
//            Assert.IsInstanceOfType(result, typeof(Student));
//            userRepositoryMock.Verify(x => x.Create(It.IsAny<BaseUser>()), Times.Once);
//        }

//        // Add more test methods as needed
//    }
//}
