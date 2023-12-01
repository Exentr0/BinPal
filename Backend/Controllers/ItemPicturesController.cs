using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Backend.Services;
using Azure;
using Backend.Services.Storage;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemPicturesController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ItemPicturesBlobService _itemPicturesBlobService;

        public ItemPicturesController(IConfiguration configuration, IUserService userService, ItemPicturesBlobService itemPicturesBlobService)
        {
            _userService = userService;
            _itemPicturesBlobService = itemPicturesBlobService;
        }

        [HttpGet("get-item-picture-urls/{itemId}")]
        public async Task<IActionResult> GetItemPictureUrls(int itemId)
        {
            try
            {
                // Get a list of URLs for pictures associated with the specified item
                var itemPictureUrls = await _itemPicturesBlobService.GetItemPictureUrlsAsync(itemId);

                // Return the list of URLs in the response
                return Ok(new { ItemPictureUrls = itemPictureUrls });
            }
            catch (InvalidOperationException ex)
            {
                // Log the error or handle it as needed
                Console.WriteLine($"Error getting item picture URLs: {ex.Message}");

                // Return a not found response (HTTP 404) if the item or pictures are not found
                return NotFound($"Item with ID {itemId} not found or item pictures not found");
            }
            catch (Exception ex)
            {
                // Log the unexpected error
                Console.WriteLine($"Unexpected error getting item picture URLs: {ex.Message}");

                // Return an internal server error response (HTTP 500)
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("upload-item-picture/{itemId}")]
        public async Task<IActionResult> UploadItemPicture(int itemId, IFormFile itemPicture)
        {
            try
            {
                // Upload the item picture to Azure Blob Storage
                await _itemPicturesBlobService.UploadBlobAsync(itemId, itemPicture);

                // Return a success response
                return Ok("Item picture uploaded successfully");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error uploading item picture: {ex.Message}");

                // Return a BadRequest result with a custom error message
                return BadRequest($"Item with ID {itemId} not found or unable to upload item picture.");
            }
            catch (RequestFailedException ex)
            {
                Console.WriteLine($"Error uploading item picture: {ex.Message}");

                // Return a StatusCode 500 (Internal Server Error) for Azure Storage-related exceptions
                return StatusCode(500, "Internal server error related to Azure Storage");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading item picture: {ex.Message}");

                // Return a StatusCode 500 (Internal Server Error) for other exceptions
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("delete-all-item-pictures/{itemId}")]
        public async Task<IActionResult> DeleteAllItemPictures(int itemId)
        {
            try
            {
                // Await the DeleteAllBlobsAsync method
                await _itemPicturesBlobService.DeleteAllBlobsAsync(itemId);

                // Return a success response
                return Ok("All item pictures deleted successfully");
            }
            catch (InvalidOperationException ex)
            {
                // Log the error or handle it as needed
                Console.WriteLine($"Error: {ex.Message}");

                // Return a not found response (HTTP 404) since the blobs were not found
                return NotFound($"Item with ID {itemId} not found or item pictures not found");
            }
            catch (RequestFailedException ex)
            {
                // Log the error or handle it as needed
                Console.WriteLine($"Error deleting blobs: {ex.Message}");

                // Return an internal server error response (HTTP 500)
                return StatusCode(500, "Internal server error");
            }
            catch (Exception ex)
            {
                // Log the unexpected error
                Console.WriteLine($"Unexpected error deleting blobs: {ex.Message}");

                // Return an internal server error response (HTTP 500)
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
