using Google.Apis.Drive.v3;

namespace VirtualTeacher
{
    public interface IGoogleDriveService
    {
        Task<List<string>> GetFileList();
        DriveService GetDriveService();
        Task<string> UploadFile(IFormFile file);

    }
}
