using Google.Apis.Auth.AspNetCore3;
using Google.Apis.Drive.v3;
using Google.Apis.Services;

namespace ReactExample
{
    public class GoogleDriveService : IGoogleDriveService
    {
        private readonly IGoogleAuthProvider _googleAuthProvider;

        public GoogleDriveService(IGoogleAuthProvider googleAuthProvider)
        {
            _googleAuthProvider = googleAuthProvider;
        }

        public DriveService GetDriveService()
        {
            var credentials = _googleAuthProvider.GetCredential().Result;

            return new DriveService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credentials,
                ApplicationName = "ReactExample",
            });
        }

        public async Task<List<string>> GetFileList()
        {
            var credential = await _googleAuthProvider.GetCredential();

            var driveService = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "ReactExample"
            });

            // Retrieve the file list
            var request = driveService.Files.List();
            var result = await request.ExecuteAsync();

            // Extract file names
            var fileNames = result.Files.Select(file => file.Name).ToList();

            return fileNames;
        }

        public async Task<string> UploadFile(IFormFile file)
        {
            var credential = await _googleAuthProvider.GetCredential();

            var driveService = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "ReactExample"
            });

            // Create a file metadata with the name of the uploaded file
            var fileMetadata = new Google.Apis.Drive.v3.Data.File
            {
                Name = file.FileName
            };

            // Upload the file
            using (var stream = file.OpenReadStream())
            {
                var request = driveService.Files.Create(fileMetadata, stream, file.ContentType);
                request.Upload();
            }

            return "File uploaded successfully!";
        }
    }
}
