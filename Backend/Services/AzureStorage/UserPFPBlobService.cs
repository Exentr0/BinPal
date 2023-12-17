using Azure;
using Azure.Storage.Blobs;
using Backend.Storage;
using Microsoft.AspNetCore.Mvc;

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
        public override async Task UploadBlobAsync(int userId, IFormFile file)
        {
            string contentType = file.ContentType;
           
               // Get the file extension from the MIME type
               string fileExtension = GetFileExtension(contentType);
           
               // Construct the blob name using the file extension
               string blobName = $"{userId}{fileExtension}";
            
            // Get the BlobClient for the specified blob
            var blobClient = ContainerClient.GetBlobClient(blobName);

            try
            {
                // Upload the profile picture to the blob
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
        
        
        // Get the URL for a user's profile picture based on user ID
        public string GetUserPfpUrl(int userId)
        {

            // Construct a prefix for the blob name using the userId
            string blobNamePrefix = $"{userId}";

            // Get all blobs with the specified prefix
            var blobs = ContainerClient.GetBlobs(prefix: $"{userId}");

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
                Console.WriteLine($"Error: No blobs found for user with ID '{userId}'.");
                return null;
            }
        }
    }
}
