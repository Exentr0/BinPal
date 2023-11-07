using Backend.Models;

namespace Backend.Services
{
    public interface IUserService
    {
        string GetMyName();
        Task Register(User user);
        Task<User> Login(string username, string password);
        Task<User> GetUser(string username);
        
    }
}