using Azure.Storage;
using Azure.Storage.Blobs;


namespace Backend.Storage;

public class AzureBlobService
{
    private readonly BlobServiceClient _blobServiceClient;

    public AzureBlobService(IConfiguration configuration)
    {
        var credential = new StorageSharedKeyCredential(configuration["AzureBlobStorageOptions:StorageAccountName"],configuration["AzureBlobStorageOptions:Key"]);
        var blobUri = $"https://{configuration["AzureBlobStorageOptions:StorageAccountName"]}.blob.core.windows.net";
        _blobServiceClient = new BlobServiceClient(new Uri(blobUri), credential);
    }

    public async Task<List<Uri>> UploadFilesAsync(string filePath)
    {
        var blobUris = new List<Uri>();
        var blobContainer = _blobServiceClient.GetBlobContainerClient("files");

        var blob = blobContainer.GetBlobClient($"test/{Path.GetFileName(filePath)}");

        await using (var fileStream = File.OpenRead(filePath))
        {
            await blob.UploadAsync(fileStream, true);
        }

        blobUris.Add(blob.Uri);

        return blobUris;
    }
}