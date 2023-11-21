using Azure;
using Azure.Storage.Blobs;
using Backend.Storage;

namespace Backend.Services.Storage
{
    public class UserPFPBlobService : AzureBlobService
    {
        // Constructor
        public UserPFPBlobService(IConfiguration configuration) : base(configuration)
        {
            // Get the container name from configuration
            ContainerName = configuration["AzureBlobStorageOptions:UserPfpContainerName"];
            
            // Get the BlobContainerClient for the specified container
            ContainerClient = BlobServiceClient.GetBlobContainerClient(ContainerName);

            // Check if the container exists
            if (!ContainerClient.Exists())
            {
                throw new InvalidOperationException($"Container '{ContainerName}' does not exist. Please create the container before using it.");
            }
        }

        // Upload a blob (profile picture) for a user
        public override async Task UploadBlobAsync(int userId, IFormFile zipFile)
        {
            string blobName = $"{userId}";
            
            // Get the BlobClient for the specified blob
            var blobClient = ContainerClient.GetBlobClient(blobName);

            try
            {
                // Upload the profile picture to the blob
                await using var stream = zipFile.OpenReadStream();
                await blobClient.UploadAsync(stream, true);
            }
            catch (RequestFailedException ex)
            {
                // Handle the exception (log, throw, etc.)
                Console.WriteLine($"Error uploading blob: {ex.Message}");
                throw;
            }
        }
        
        
        // Get the URL for a user's profile picture based on user ID
        public string GetUserPfpUrl(int userId)
        {
            string blobName = $"{userId}";

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
}
