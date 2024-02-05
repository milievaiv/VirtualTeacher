using Google.Apis.Auth.AspNetCore3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Util.Store;

namespace VirtualTeacher
{
    public class GoogleAuthProvider : IGoogleAuthProvider
    {
        private readonly IConfiguration _configuration;

        public GoogleAuthProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<UserCredential> GetCredential()
        {
            try
            {
                using (var stream = new FileStream("C:\\Google Keys\\credentials.json", FileMode.Open, FileAccess.Read))
                {
                    string credPath = "token.json";
                    return await GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        new[] { DriveService.Scope.Drive },
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)
                    );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Authorization failed: {ex.Message}");
                throw;
            }
        }
    }
}
