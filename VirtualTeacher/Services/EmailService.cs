using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using VirtualTeacher.Services.Contracts;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit;
using VirtualTeacher.Models.DTO;
using System.Text;
using PhotoForum.Exceptions;

namespace VirtualTeacher.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public void SendVerificationEmail(string requestId, TeacherCandidate contents)
        {
            try
            {
                using (SmtpClient client = new SmtpClient(_smtpSettings.Server, _smtpSettings.Port))
                {
                    client.EnableSsl = _smtpSettings.UseSsl;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password);

                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress(_smtpSettings.Username);
                        mailMessage.To.Add(contents.Email);
                        mailMessage.Subject = $"Verify Your Application #{requestId}";

                        StringBuilder sb = new StringBuilder();

                        sb.AppendLine("Hello,");
                        sb.AppendLine("We're glad to know that you have interest in becoming a part of our team.");
                        sb.AppendLine();
                        sb.AppendLine("Please, verify your application by clicking on the following link:");

                        sb.AppendLine($"https://localhost:5267/api/teacher-candidates/verify-submission?requestId={requestId}");

                        mailMessage.Body = sb.ToString();

                        client.Send(mailMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending verification email: {ex.Message}");
            }
        }

        public async Task FindEmail(string requestId)
        {
            using (var client = new ImapClient())
            {
                await client.ConnectAsync(_smtpSettings.Server, 993, true);
                await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);

                // Select the Inbox folder
                var sentFolder = client.GetFolder(SpecialFolder.Sent);
                await sentFolder.OpenAsync(FolderAccess.ReadOnly);

                // Search for emails with a specific subject and from the application's email address
                var searchQuery = SearchQuery.SubjectContains(requestId)
                                .And(SearchQuery.FromContains(_smtpSettings.Username));

                var results = await sentFolder.SearchAsync(searchQuery);

                await client.DisconnectAsync(true);

                if (results.Count == 0) throw new EntityNotFoundException("Email with given id does not exist.");
            }
        }
    }
}
