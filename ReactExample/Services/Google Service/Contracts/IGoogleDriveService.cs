using Google.Apis.Drive.v3;

namespace ReactExample
{
    public interface IGoogleDriveService
    {
        Task<List<string>> GetFileList();
        DriveService GetDriveService();
        Task<string> UploadFile(IFormFile file);

    }
}
