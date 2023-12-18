using System.Security.Claims;
using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Backend.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _dataContext;

        public UserProfileService(IHttpContextAccessor httpContextAccessor, DataContext dataContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _dataContext = dataContext; 
        }
        
        public async Task<User> GetUserProfile(int userId)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null)
            {
                _dataContext.Entry(user).Property(u => u.Username);
                _dataContext.Entry(user).Property(u => u.AvatarUrl);
                _dataContext.Entry(user).Property(u => u.Bio);
                _dataContext.Entry(user).Property(u => u.MainVideo);
                _dataContext.Entry(user).Property(u => u.Email);
                await _dataContext.Entry(user).Reference(u => u.ShoppingCart).LoadAsync();
                await _dataContext.Entry(user).Collection(u => u.PaymentMethods).LoadAsync();
                await _dataContext.Entry(user).Collection(u => u.PublishedPackages).LoadAsync();
                await _dataContext.Entry(user).Collection(u => u.Purchases).LoadAsync();
                await _dataContext.Entry(user).Collection(u => u.ItemReviews).LoadAsync();
            }

            return user;
        }
        
        public async Task AddComment(int commenterUserId, int commentedUserId, string text, float rating, string username, string avatarUrl)
        {
            var comment = new Comment
            {
                CommenterUserId = commenterUserId,
                CommentedUserId = commentedUserId,
                Text = text,
                Rating = rating,
                CreatedAt = DateTime.UtcNow,
                Username = username,
                AvatarUrl = avatarUrl
            };

            _dataContext.Comments.Add(comment);
            await _dataContext.SaveChangesAsync();
        }
        
        public async Task<List<Comment>> GetComments(int userId)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null)
            {
                _dataContext.Entry(user).Property(u => u.Username);
                await _dataContext.Entry(user).Collection(u => u.CommentsReceived).LoadAsync();
            }
            
            return user?.CommentsReceived ?? new List<Comment>();
        }
    }
}