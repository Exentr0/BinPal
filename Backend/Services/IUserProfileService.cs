using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services
{
    public interface IUserProfileService
    {
        Task<User> GetUserProfile(int userId);
        Task AddComment(int commenterUserId, int commentedUserId, string text, float rating, string username, string avatarUrl);
        Task<List<Comment>> GetComments(int userId);
    }
}