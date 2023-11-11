using System.Net;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Identity.Client.Extensions.Msal;

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

    public async Task<List<Uri>> UploadFilesAsync()
    {
        var blobUris = new List<Uri>(); 
        string filePath = "Test.txt";
        var blobContainer = _blobServiceClient.GetBlobContainerClient("files");

        var blob = blobContainer.GetBlobClient($"test/{filePath}");

        await blob.UploadAsync(filePath, true);
        blobUris.Add(blob.Uri);

        return blobUris;
    }
}