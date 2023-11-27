using Azure;
using Backend.Storage;

namespace Backend.Services.Storage;

public class ItemReleasesBlobService : AzureBlobService
{
    public ItemReleasesBlobService(IConfiguration configuration) : base(configuration)
    {
        ContainerName = configuration["AzureBlobStorageOptions:ItemContentContainerName"];
        
        ContainerClient = BlobServiceClient.GetBlobContainerClient(ContainerName);
        
        if (!ContainerClient.Exists())
        {
            throw new InvalidOperationException($"Container '{ContainerName}' does not exist. Please create the container before using it.");
        }
    }

    public override async Task UploadBlobAsync(int itemId, IFormFile file)
    {
        // Construct the blob name with the specified directory (ItemId)
        var blobName = $"{itemId}/{Path.GetFileName(file.FileName)}";

        // Get the BlobClient for the specified blob
        var blockBlobClient = ContainerClient.GetBlobClient(blobName);

        try
        {
            // Upload the file to the blob
            await using var stream = file.OpenReadStream();
            await blockBlobClient.UploadAsync(stream, true);
        }
        catch (RequestFailedException ex)
        {
            Console.WriteLine($"Error uploading blob: {ex.Message}");
            throw;
        }
    }
    
    //Delete all blobs of releases of Item
    public async Task DeleteAllBlobsAsync(int itemId)
    {
        string directory = $"{itemId}";
        await foreach (var blobItem in ContainerClient.GetBlobsAsync(prefix: directory))
        {
            await DeleteBlobAsync(blobItem.Name);
        }
    }
    
    //Get Urls to blobs of Releases of Item
    public async Task<List<string>> GetItemReleaseUrlsAsync(int itemId)
    {
        string directory = $"{itemId}";
        var blobUrls = new List<string>();

        await foreach (var blobItem in ContainerClient.GetBlobsAsync(prefix: directory))
        {
            var blobName = blobItem.Name;
            var blobClient = ContainerClient.GetBlobClient(blobName);

            // Construct the URL based on the blob client's Uri
            var blobUrl = blobClient.Uri.ToString();

            blobUrls.Add(blobUrl);
        }

        return blobUrls;
    }
}