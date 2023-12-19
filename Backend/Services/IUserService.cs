using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services
{
    public interface IUserService
    {
        string GetMyName();
        int GetUserIdFromToken();
        string GetMyAvatarUrl();
        Task<User> Register(User user);
        Task<User> Login(string username, string password);
        Task<User> GetUserByName(string username);
    }
}