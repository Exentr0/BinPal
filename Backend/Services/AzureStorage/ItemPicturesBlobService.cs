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
    public override async Task UploadBlobAsync(int itemId, IFormFile file)
    {
        string contentType = file.ContentType;
           
        // Get the file extension from the MIME type
        string fileExtension = GetFileExtension(contentType);

        // Construct the blob name with the specified directory (ItemId)

        var blobName = $"{itemId}/{Path.GetFileName(file.FileName)}{fileExtension}";

                
        // Get the BlobClient for the specified blob
        var blobClient = ContainerClient.GetBlobClient(blobName);
 
        try
        {
            // Upload the image to the blob
            await using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream, true);
        }
        catch (RequestFailedException ex)
        {
            Console.WriteLine($"Error uploading blob: {ex.Message}");
            throw;
        }
    }

    // Delete all blobs (pictures) for a specified item
    public async Task DeleteAllBlobsAsync(int itemId)
    {
        string directory = $"{itemId}";

        // Iterate through all blobs in the specified directory and delete them
        await foreach (var blobItem in ContainerClient.GetBlobsAsync(prefix: directory))
        {
            await DeleteBlobAsync(blobItem.Name);
        }
    }

    // Get a list of URLs for pictures associated with a specified item
    public async Task<List<string>> GetItemPictureUrlsAsync(int itemId)
    {
        string directory = $"{itemId}";
        var blobUrls = new List<string>();

        // Iterate through all blobs in the specified directory and construct URLs
        await foreach (var blobItem in ContainerClient.GetBlobsAsync(prefix: directory))
        {
            var blobName = blobItem.Name;
            var blobClient = ContainerClient.GetBlobClient(blobName);

            // Construct the URL based on the blob client's Uri
            var blobUrl = blobClient.Uri.ToString();

            // Add the URL to the list
            blobUrls.Add(blobUrl);
        }

        // Return the list of URLs
        return blobUrls;
    }
}