using System.Security.Claims;
using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Backend.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _dataContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private IUserProfileService _userProfileServiceImplementation;

        public UserProfileService(IHttpContextAccessor httpContextAccessor, DataContext dataContext,IPasswordHasher<User> passwordHasher)
        {
            _httpContextAccessor = httpContextAccessor;
            _dataContext = dataContext; 
            _passwordHasher = passwordHasher;
        }
        
        public async Task<User> GetUserProfile(int userId)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null)
            {
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
        

        // Profile edit
        public User GetUserById(int userId)
        {
            return _dataContext.Users.Find(userId);
        }

        public async Task UpdateUser(int userId, UpdateUser model)
        {
            var user = await _dataContext.Users.FindAsync(userId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            // Оновлення інших полів користувача
            user.Username = model.Username;
            user.Email = model.Email;
            user.Bio = model.Bio;
            user.MainVideo = model.MainVideo;
            user.AvatarUrl = model.AvatarUrl;

            // Зміна паролю, якщо вказано новий пароль
            if (!string.IsNullOrWhiteSpace(model.NewPassword))
            {
                // Перевірка поточного паролю
                var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.Password, model.CurrentPassword);

                if (passwordVerificationResult == PasswordVerificationResult.Failed)
                {
                    throw new Exception("Incorrect current password");
                }

                // Генерація нового хеша для нового паролю
                var newPasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);

                // Оновлення паролю в базі даних
                user.Password = newPasswordHash;
            }
            
            _dataContext.SaveChanges();
        }
        
        public async Task DeleteUser(int userId)
        {
            try
            {
                var user = await _dataContext.Users.FindAsync(userId);

                if (user == null)
                {
                    throw new Exception("User not found");
                }
                
                // Видалення коментарів, які посилаються на цього користувача
                var commentsToDelete = _dataContext.Comments.Where(c => c.CommenterUserId == userId || c.CommentedUserId == userId);
                _dataContext.Comments.RemoveRange(commentsToDelete);

                _dataContext.Users.Remove(user);
                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Console.WriteLine("Inner Exception: " + ex.InnerException?.Message);
                throw; // Передача виключення у контролер
            }
        }
    }
}