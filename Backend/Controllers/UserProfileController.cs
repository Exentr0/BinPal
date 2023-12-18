using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class CommentRequestModel
    {
        public int CommentedUserId { get; set; }
        public string Text { get; set; }
        public float Rating { get; set; }
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
                
                return Ok(userProfile);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound($"User with ID {userId} not found. {ex.Message}");
            }
        }
        
        [HttpPost("user-profile/add-comment")]
        [Authorize]
        public async Task<IActionResult> AddComment([FromBody] CommentRequestModel commentRequest)
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
    }
}