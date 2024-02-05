using Google.Apis.Auth.OAuth2;

namespace VirtualTeacher
{
    public interface IGoogleAuthProvider
    {
        Task<UserCredential> GetCredential();
    }
}
