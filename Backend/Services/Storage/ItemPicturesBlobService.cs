using Azure;
using Azure.Storage.Blobs;
using Backend.Storage;

namespace Backend.Services.Storage;

public class ItemPicturesBlobService : AzureBlobService
{
    
    public ItemPicturesBlobService(IConfiguration configuration) : base(configuration)
    {
        ContainerName = configuration["AzureBlobStorageOptions:ItemPicturesContainerName"];
        
        ContainerClient = BlobServiceClient.GetBlobContainerClient(ContainerName);
        
        if (!ContainerClient.Exists())
        {
            throw new InvalidOperationException($"Container '{ContainerName}' does not exist. Please create the container before using it.");
        }
    }

    // Upload a blob (picture) for a Item
    public override async Task UploadBlobAsync(int itemId, IFormFile zipFile)
    {
        // Construct the blob name with the specified directory (ItemId)
        var blobName = $"{itemId}/{Path.GetFileName(zipFile.FileName)}";
                
        // Get the BlobClient for the specified blob
        var blobClient = ContainerClient.GetBlobClient(blobName);

        try
        {
            // Upload the image to the blob
            await using var stream = zipFile.OpenReadStream();
            await blobClient.UploadAsync(stream, true);
        }
        catch (RequestFailedException ex)
        {
            Console.WriteLine($"Error uploading blob: {ex.Message}");
            throw;
        }
    }
}