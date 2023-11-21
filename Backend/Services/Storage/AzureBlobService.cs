using Azure;
using Azure.Storage;
using Azure.Storage.Blobs;

namespace Backend.Storage
{
    public abstract class AzureBlobService
    {
        // BlobServiceClient for interacting with Azure Blob Storage
        protected readonly BlobServiceClient BlobServiceClient;

        // Container name for storing blobs
        protected string ContainerName;

        // BlobContainerClient for interacting with a specific container
        protected BlobContainerClient ContainerClient;
        
        //Constructor
        protected AzureBlobService(IConfiguration configuration)
        {
            // Create StorageSharedKeyCredential using Azure Storage account name and key
            var credential = new StorageSharedKeyCredential(
                configuration["AzureBlobStorageOptions:StorageAccountName"],
                configuration["AzureBlobStorageOptions:Key"]
            );

            // Build the BlobServiceClient with the Storage account URI and credentials
            var blobUri = $"https://{configuration["AzureBlobStorageOptions:StorageAccountName"]}.blob.core.windows.net";
            BlobServiceClient = new BlobServiceClient(new Uri(blobUri), credential);
        }

        //Abstract method for uploading a blob
        public abstract Task UploadBlobAsync(int entityId, IFormFile zipFile);

     
        // Delete a blob based on the blob name
       public async Task DeleteBlobAsync(string blobName)
       {
           try
           {
               // Get the BlobClient for the specified blob
               var blobClient = ContainerClient.GetBlobClient(blobName);
       
               // Delete the blob if it exists
               if (await blobClient.ExistsAsync())
               {
                   await blobClient.DeleteIfExistsAsync();
                   Console.WriteLine($"Blob with name '{blobName}' deleted successfully.");
               }
               else
               {
                   // Log the error and throw an exception
                   Console.WriteLine($"Error: Blob with name '{blobName}' not found.");
                   throw new InvalidOperationException($"Blob with name '{blobName}' not found.");
               }
           }
           catch (RequestFailedException ex)
           {
               Console.WriteLine($"Error deleting blob: {ex.Message}");
               // Log the error or rethrow the exception based on your application's needs
               throw;
           }
           catch (Exception ex)
           {
               Console.WriteLine($"Unexpected error deleting blob: {ex.Message}");
               // Log the error or rethrow the exception based on your application's needs
               throw;
           }
       }

    }
}
