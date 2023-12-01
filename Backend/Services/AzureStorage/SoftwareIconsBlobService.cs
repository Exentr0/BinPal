using Azure;
using Backend.Storage;

namespace Backend.Services.Storage;

public class SoftwarePicturesBlobService : AzureBlobService
{
    public SoftwarePicturesBlobService(IConfiguration configuration) : base(configuration)
    {
        ContainerName = configuration["AzureBlobStorageOptions:SoftwarePicturesContainerName"];
        
        ContainerClient = BlobServiceClient.GetBlobContainerClient(ContainerName);
        
        if (!ContainerClient.Exists())
        {
            throw new InvalidOperationException($"Container '{ContainerName}' does not exist. Please create the container before using it.");
        }
    }

    public override async Task UploadBlobAsync(int softwareId, IFormFile file)
    {
        string contentType = file.ContentType;
           
        // Get the file extension from the MIME type
        string fileExtension = GetFileExtension(contentType);

        string blobName = $"{softwareId}{fileExtension}";
            
        // Get the BlobClient for the specified blob
        var blobClient = ContainerClient.GetBlobClient(blobName);

        try
        {
            // Upload the software picture to the blob
            await using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream, true);
        }
        catch (RequestFailedException ex)
        {
            // Handle the exception (log, throw, etc.)
            Console.WriteLine($"Error uploading blob: {ex.Message}");
            throw;
        }
    }
    
    // Get the URL for a software picture based on software ID
    public string GetSoftwarePictureUrl(int softwareId)
    {
        // Construct a prefix for the blob name using the softwareId
        string blobNamePrefix = $"{softwareId}";

        // Get all blobs with the specified prefix
        var blobs = ContainerClient.GetBlobs(prefix: blobNamePrefix);

        // Check if any blobs exist with the specified prefix
        if (blobs.Any())
        {
            // Retrieve the URL for the first blob (you may want to handle multiple blobs differently)
            var blobClient = ContainerClient.GetBlobClient(blobs.First().Name);
            var blobUrl = blobClient.Uri.ToString();
            return blobUrl;
        }
        else
        {
            // No blobs found
            Console.WriteLine($"Error: No blobs found for software with ID '{softwareId}'.");
            return null;
        }
    }
}