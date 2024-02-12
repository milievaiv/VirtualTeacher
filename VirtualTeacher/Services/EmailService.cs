using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.Extensions.Options;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Models.DTO.TeacherDTO;
using HtmlAgilityPack;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using VirtualTeacher.Models.DTO.TeacherDTO;

namespace VirtualTeacher.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;
        private const string Submissions = "Submissions";
        private HashSet<string> UserFlags
        {
            get
            {
                return new HashSet<string>() { Submissions };
            }
        }
        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public void SendVerificationEmail(string requestId, TeacherCandidateDto contents)
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
                        mailMessage.Subject = $"Your Application #{requestId}";

                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("<div>");
                        sb.AppendLine("    <p>Hello,</p>");
                        sb.AppendLine("    <p>We're glad to know that you have interest in becoming a part of our team.</p>");
                        sb.AppendLine("    <p>Please, verify your application by clicking on the following link:</p>");
                        sb.AppendLine($"   <p><a href=\"http://localhost:5267/api/teacher-candidates/verify-submission?requestId={requestId}\" target=\"_blank\">http://localhost:5267/api/teacher-candidates/verify-submission?requestId={requestId}</a></p>");
                        // Add more lines as needed
                        sb.AppendLine(" <div id=\"applicationInformation\">");
                        sb.AppendLine("    <h3>Application Information:</h3>");
                        sb.AppendLine("    <p style=\"font-style: normal; font-weight: bold;\">Title: ");
                        sb.AppendLine($"        <span style=\"font-weight: normal; font-style: oblique;\">" + contents.Title + "</span></p>");
                        sb.AppendLine("    <p style=\"font-style: normal; font-weight: bold;\">Email:");
                        sb.AppendLine($"        <span style=\"font-weight: normal; font-style: normal;\">" + contents.Email + "</span></p>");
                        sb.AppendLine("    <p style=\"font-weight: bold;\">Message:</p>");
                        sb.AppendLine("    <p style=\"border-style: ridge; padding: 2vh;\"> " + contents.Message.Replace("\n", "<br>") + "</p>");
                        sb.AppendLine(" </div>");
                        sb.AppendLine("</div>");


                        //sb.AppendLine("<html><body>");
                        //sb.AppendLine("<p>Hello,</p>");
                        //sb.AppendLine("<p>We're glad to know that you have interest in becoming a part of our team.</p>");
                        //sb.AppendLine("<p>Please, verify your application by clicking on the following link:</p>");
                        //sb.AppendLine($"<p><a href=\"http://localhost:5267/api/teacher-candidates/verify-submission?requestId={requestId}\">Verification Link</a></p>");

                        //// Attach contents as a plain text file
                        //sb.AppendLine("<p>Your Application Information:</p>");
                        //sb.AppendLine("<p>Title: " + contents.Title + "</p>");
                        //sb.AppendLine("<p>Email: " + contents.Email + "</p>");
                        //sb.AppendLine("<p>Message:</p>");
                        //sb.AppendLine("<p>" + contents.Message.Replace("\n", "</p>") + "</p>");

                        sb.AppendLine("</body></html>");

                        mailMessage.Body = sb.ToString();
                        mailMessage.IsBodyHtml = true;

                        client.Send(mailMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending verification email: {ex.Message}");
            }
        }

        public async Task VerifyApplication(string requestId)
        {
            MimeMessage message = new MimeMessage();
            using (var client = new ImapClient())
            {
                await client.ConnectAsync(_smtpSettings.Server, 993, true);
                await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);

                // Select the Inbox folder
                var sentFolder = client.GetFolder(SpecialFolder.Sent);
                await sentFolder.OpenAsync(FolderAccess.ReadWrite);

                // Search for emails with a specific subject and from the application's email address
                var searchQuery = SearchQuery.SubjectContains(requestId)
                                .And(SearchQuery.FromContains(_smtpSettings.Username));

                var results = await sentFolder.SearchAsync(searchQuery);

                if (results.Count == 0)
                {
                    await client.DisconnectAsync(true);
                    throw new EntityNotFoundException("Email with given id does not exist.");
                }

                var messageId = results[0];
                message = sentFolder.GetMessage(messageId);

                sentFolder.AddFlags(messageId, MessageFlags.Flagged, false);

                await sentFolder.ExpungeAsync();
                client.Disconnect(true);
            }

            // Remove carriage return, line feed, and equals sign symbols
            string cleanedHtml = CleanHtml(message.HtmlBody);

            // Load the cleaned HTML content
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(cleanedHtml);

            // Use XPath to select the div with id="applicationInformation"
            HtmlNode applicationInformationDiv = htmlDocument.GetElementbyId("applicationInformation");

            string applicationInformationContent = applicationInformationDiv.InnerHtml;

            using (SmtpClient client = new SmtpClient(_smtpSettings.Server, _smtpSettings.Port))
            {
                client.EnableSsl = _smtpSettings.UseSsl;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password);

                using (MailMessage forwardedMessage = new MailMessage())
                {
                    forwardedMessage.From = new MailAddress(_smtpSettings.Username);
                    forwardedMessage.To.Add("virtualteacher.official@gmail.com");
                    forwardedMessage.Subject = $"Application #{requestId}";




                    forwardedMessage.Body = applicationInformationContent;
                    forwardedMessage.IsBodyHtml = true;

                    client.Send(forwardedMessage);
                }
            }
        }

        private static string CleanHtml(string html)
        {
            // Remove carriage return and line feed
            html = html.Replace("\r", "").Replace("\n", "");

            return html;
        }
    }
}
