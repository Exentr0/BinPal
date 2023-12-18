using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class UserEditModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Bio { get; set; }
        public string MainVideo { get; set; }
        public string AvatarUrl { get; set; }
    }
    
    [Route("api/[controller]")]
    [ApiController]

    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IUserService _userService;
        private readonly ILogger<UserProfileController> _logger;

        public UserProfileController(IConfiguration configuration, IUserProfileService userProfileService, IUserService userService,ILogger<UserProfileController> logger)
        {
            _userProfileService = userProfileService;
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("user-profile/{userId}")]
        public async Task<IActionResult> GetUserProfile(int userId)
        {
            try
            {
                var userProfile = await _userProfileService.GetUserProfile(userId);
                
                if (userProfile == null)
                {
                    // Обробка випадку, коли продавець не знайдений
                    return NotFound();
                }
                
                // return Ok(userProfile);
                return new JsonResult(new
                {
                    userProfile.Id,
                    userProfile.Username,
                    userProfile.Bio,
                    userProfile.AvatarUrl,
                    userProfile.Email,
                    userProfile.MainVideo,
                    userProfile.Purchases,
                    userProfile.ItemReviews,
                    userProfile.PublishedPackages
                });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound($"User with ID {userId} not found. {ex.Message}");
            }
        }
        
        // Comments
        [HttpPost("user-profile/add-comment")]
        [Authorize]
        public async Task<IActionResult> AddComment([FromBody] CommentRequest commentRequest)
        {
            try
            {
                int commenterUserId = _userService.GetUserIdFromToken(); 
                int commentedUserId = commentRequest.CommentedUserId;
                string text = commentRequest.Text;
                float rating = commentRequest.Rating;
                string username = _userService.GetMyName();
                string avatarUrl = _userService.GetMyAvatarUrl();
                
                await _userProfileService.AddComment(commenterUserId, commentedUserId, text, rating, username, avatarUrl);
                
                return Ok("Comment added successfully");
            }
    
            catch (Exception ex)
            {
                _logger.LogError(ex, "Під час збереження змін сутності коментаря виникла помилка");
                throw;
            }
            
        }

        [HttpGet("user-profile/{userId}/comments")]
        public async Task<IActionResult> GetComments(int userId)
        {
            try
            {
                var comments = await _userProfileService.GetComments(userId);
                return Ok(comments);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound($"User with ID {userId} not found. {ex.Message}");
            }
        }
        
        // Edit protile
        [HttpPut("user-profile/{userId}")]
        [Authorize]
        public IActionResult UpdateUser([FromBody] UpdateUser model)
        {
            try
            {
                int userId =  _userService.GetUserIdFromToken(); 
               
                _userProfileService.UpdateUser(userId, model);

                return Ok("User updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating user: {ex.Message}");
            }
        }
        
        [HttpDelete("user-profile/delete")]
        public async Task<IActionResult> DeleteUser()
        {
            try
            {
                int userId = _userService.GetUserIdFromToken();
                await _userProfileService.DeleteUser(userId);
                return Ok("User was deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {
                await _userProfileService.DeleteUser(userId);
                return Ok("User was deleted");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Console.WriteLine("Inner Exception: " + ex.InnerException?.Message);
                return BadRequest(new { message = ex.Message });
            }
        }
        
    }
}