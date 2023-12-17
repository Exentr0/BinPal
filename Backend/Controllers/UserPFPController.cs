using Azure;
using Backend.Services;
using Backend.Services.Storage;
using Microsoft.AspNetCore.Mvc;


namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserPFPController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserPFPBlobService _userPfpBlobService;

        public UserPFPController(IConfiguration configuration, IUserService userService, UserPFPBlobService userPfpBlobService)
        {
            _userService = userService;
            _userPfpBlobService = userPfpBlobService;
        }
        
        [HttpGet("get-profile-picture-url/{userId}")]
        public IActionResult GetPFPUrl(int userId)
        {
            try
            {
                // Get the URL for the user's profile picture
                var profilePictureUrl = _userPfpBlobService.GetUserPfpUrl(userId);

                // Return the profile picture URL in the response
                return Ok(new { ProfilePictureUrl = profilePictureUrl });
            }
            catch (InvalidOperationException ex)
            {
                // Log the error or handle it as needed
                Console.WriteLine($"Error getting profile picture URL: {ex.Message}");

                // Return a not found response (HTTP 404) if the user or profile picture is not found
                return NotFound($"User with ID {userId} not found or profile picture not found");
            }
            catch (Exception ex)
            {
                // Log the unexpected error
                Console.WriteLine($"Unexpected error getting profile picture URL: {ex.Message}");

                // Return an internal server error response (HTTP 500)
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPost("change-profile-picture/{userId}")]
        public async Task<IActionResult> UpdatePFP(int userId, IFormFile profilePicture)
        {
            try
            {
                // Upload the profile picture to Azure Blob Storage
                await _userPfpBlobService.UploadBlobAsync(userId, profilePicture);

                // Get the URL for the user's profile picture
                var newProfilePictureUrl = _userPfpBlobService.GetUserPfpUrl(userId);

                return Ok("Profile picture updated successfully");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error changing profile picture: {ex.Message}");

                // Return a BadRequest result with a custom error message
                return BadRequest($"User with ID {userId} not found or unable to update profile picture.");
            }
            catch (RequestFailedException ex)
            {
                Console.WriteLine($"Error changing profile picture: {ex.Message}");

                // Return a StatusCode 500 (Internal Server Error) for Azure Storage-related exceptions
                return StatusCode(500, "Internal server error related to Azure Storage");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error changing profile picture: {ex.Message}");

                // Return a StatusCode 500 (Internal Server Error) for other exceptions
                return StatusCode(500, "Internal server error");
            }
        }

        // Delete userPfp from AzureStorage
        [HttpPost("delete-profile-picture/{userId}")]
        public async Task<IActionResult> DeletePFP(int userId)
        {
            try
            {
                // Await the DeleteBlobAsync method
                await _userPfpBlobService.DeleteBlobAsync($"{userId}");

                // Return a success response
                return Ok("Profile picture deleted successfully");
            }
            catch (InvalidOperationException ex)
            {
                // Log the error or handle it as needed
                Console.WriteLine($"Error: {ex.Message}");

                // Return a not found response (HTTP 404) since the blob was not found
                return NotFound($"userId was not found or profile picture for user with ID {userId} not found");
            }
            catch (RequestFailedException ex)
            {
                // Log the error or handle it as needed
                Console.WriteLine($"Error deleting blob: {ex.Message}");

                // Return an internal server error response (HTTP 500)
                return StatusCode(500, "Internal server error");
            }
            catch (Exception ex)
            {
                // Log the unexpected error
                Console.WriteLine($"Unexpected error deleting blob: {ex.Message}");

                // Return an internal server error response (HTTP 500)
                return StatusCode(500, "Internal server error");
            }
        }
    }
}