using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services
{
    public interface IUserProfileService
    {
        Task<User> GetUserProfile(int userId);
        Task AddComment(int commenterUserId, int commentedUserId, string text, float rating, string username, string avatarUrl);
        Task<List<Comment>> GetComments(int userId);
        Task UpdateUser(int userId, UpdateUser model);
        User GetUserById(int userId);
        // Task ChangePassword(int userId, string currentPassword, string newPassword);
        Task DeleteUser(int userId);

    }
}