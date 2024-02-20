using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using System;

public class CloudStorageService
{
    private readonly StorageClient _storageClient;
    private readonly string _bucketName;

    public CloudStorageService(string serviceAccountKeyPath, string bucketName)
    {
        GoogleCredential credential = GoogleCredential.FromFile(serviceAccountKeyPath);
        _storageClient = StorageClient.Create(credential);
        _bucketName = bucketName;
    }

    public List<string> GetFileList(string folderPath)
    {
        var objects = _storageClient.ListObjects(_bucketName, folderPath);
        var fileList = new List<string>();

        foreach (var obj in objects)
        {
            string fileName = Path.GetFileName(obj.Name);

            if (!(fileName == ""))
            {
                fileList.Add(fileName);
            }
        }

        return fileList;
    }

    public byte[] GetImageContent(string imageName, string folderPath)
    {
        try
        {
            // Construct the object name based on the provided imageName
            string objectName = folderPath + imageName;

            // Fetch the object (image) content
            using (var stream = new MemoryStream())
            {
                _storageClient.DownloadObject(_bucketName, objectName, stream);
                return stream.ToArray();
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public byte[] GetFileContent(string folderPath)
    {
        // Create a memory stream to capture the file content
        using (var memoryStream = new MemoryStream())
        {
            // Download the file content from Google Cloud Storage to the memory stream
            _storageClient.DownloadObject(_bucketName, folderPath, memoryStream);

            // Reset the memory stream position to the beginning
            memoryStream.Position = 0;

            // Read the content from the memory stream and return it as a byte array
            using (var reader = new BinaryReader(memoryStream))
            {
                return reader.ReadBytes((int)memoryStream.Length);
            }
        }
    }
    public async Task UploadFileAsync(string objectName, Stream fileStream)
    {
        // Your existing code for uploading the file goes here

        await _storageClient.UploadObjectAsync(
            _bucketName,
            objectName,
            null,
            fileStream
        );
    }   

}
