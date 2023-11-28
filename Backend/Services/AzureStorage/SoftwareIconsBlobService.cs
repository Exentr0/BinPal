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
        string blobName = $"{softwareId}";
            
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
        string blobName = $"{softwareId}.png";

        // Get the BlobClient for the specified blob
        var blobClient = ContainerClient.GetBlobClient(blobName);

        try
        {
            // Check if the blob exists before trying to retrieve its URL
            if (blobClient.Exists())
            {
                var blobUrl = blobClient.Uri.ToString();
                return blobUrl;
            }
            else
            {
                // Blob not found
                Console.WriteLine($"Error: Blob with name '{blobName}' not found.");
                return null;
            }
        }
        catch (RequestFailedException ex)
        {
            // Handle the exception (log, throw, etc.)
            Console.WriteLine($"Error checking blob existence: {ex.Message}");
            return null;
        }
    }
}