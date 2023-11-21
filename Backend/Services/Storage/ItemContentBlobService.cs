using Azure;
using Backend.Storage;

namespace Backend.Services.Storage;

public class ItemContentBlobService : AzureBlobService
{
    public ItemContentBlobService(IConfiguration configuration) : base(configuration)
    {
        ContainerName = configuration["AzureBlobStorageOptions:ItemContentContainerName"];
        
        ContainerClient = BlobServiceClient.GetBlobContainerClient(ContainerName);
        
        if (!ContainerClient.Exists())
        {
            throw new InvalidOperationException($"Container '{ContainerName}' does not exist. Please create the container before using it.");
        }
    }

    public override async Task UploadBlobAsync(int itemId, IFormFile zipFile)
    {
        // Construct the blob name with the specified directory (ItemId)
        var blobName = $"{itemId}/{Path.GetFileName(zipFile.FileName)}";

        // Get the BlobClient for the specified blob
        var blockBlobClient = ContainerClient.GetBlobClient(blobName);

        try
        {
            // Upload the file to the blob
            await using var stream = zipFile.OpenReadStream();
            await blockBlobClient.UploadAsync(stream, true);
        }
        catch (RequestFailedException ex)
        {
            Console.WriteLine($"Error uploading blob: {ex.Message}");
            throw;
        }
    }
    
    public async Task DeleteAllBlobsAsync(int itemId)
    {
        string directory = $"{itemId}";
        await foreach (var blobItem in ContainerClient.GetBlobsAsync(prefix: directory))
        {
            await DeleteBlobAsync(blobItem.Name);
        }
    }
}