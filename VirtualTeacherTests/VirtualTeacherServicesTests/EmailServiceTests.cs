using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Threading.Tasks;
using VirtualTeacher;
using VirtualTeacher.Models.DTO.TeacherDTO;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Services;
using Xunit;

namespace VirtualTeacherServicesTests
{
    [TestClass]
    public class EmailServiceTests
    {
        [TestMethod]
        public void SendVerificationEmail_Sends_Email_Successfully()
        {
            // Arrange
            var teacherRepositoryMock = new Mock<ITeacherRepository>();
            var smtpSettings = new SmtpSettings
            {
                Server = "smtp.example.com",
                Port = 587,
                Username = "test@example.com",
                Password = "password",
                UseSsl = true
            };
            var smtpSettingsOptionsMock = new Mock<IOptions<SmtpSettings>>();
            smtpSettingsOptionsMock.Setup(x => x.Value).Returns(smtpSettings);

            var emailService = new EmailService(teacherRepositoryMock.Object, smtpSettingsOptionsMock.Object);

            var requestId = Guid.NewGuid().ToString();
            var contents = new TeacherCandidateDto
            {
                Email = "recipient@example.com",
                Title = "Title",
                Message = "Test message"
            };

            // Act
            emailService.SendVerificationEmail(requestId, contents);

        }


        //Needs real data.
        //[TestMethod]
        //public async Task VerifyApplication_Verifies_Application_Successfully()
        //{
        //    // Arrange
        //    var teacherRepositoryMock = new Mock<ITeacherRepository>();
        //    var smtpSettings = new SmtpSettings
        //    {
        //        Server = "imap.example.com",
        //        Port = 993,
        //        Username = "test@example.com",
        //        Password = "password",
        //        UseSsl = true
        //    };
        //    var smtpSettingsOptionsMock = new Mock<IOptions<SmtpSettings>>();
        //    smtpSettingsOptionsMock.Setup(x => x.Value).Returns(smtpSettings);

        //    var emailService = new EmailService(teacherRepositoryMock.Object, smtpSettingsOptionsMock.Object);

        //    var requestId = Guid.NewGuid().ToString();

        //    // Act
        //    await emailService.VerifyApplication(requestId);

        //}
    }
}
