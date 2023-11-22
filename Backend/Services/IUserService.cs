using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services
{
    public interface IUserService
    {
        string GetMyName();
        Task<User> Register(User user);
        Task<User> Login(string username, string password);
        Task<User> GetUserByName(string username);
        Task UpdatePFP(int userId, string pfpUrl);
    }
}