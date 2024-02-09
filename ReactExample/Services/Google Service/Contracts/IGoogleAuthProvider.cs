using Google.Apis.Auth.OAuth2;

namespace ReactExample
{
    public interface IGoogleAuthProvider
    {
        Task<UserCredential> GetCredential();
    }
}
