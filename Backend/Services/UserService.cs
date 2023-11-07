using System.Security.Claims;
using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Backend.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _dataContext;

        public UserService(IHttpContextAccessor httpContextAccessor, DataContext dataContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _dataContext = dataContext; 
        }

        public string GetMyName()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext is not null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }

            return result;
        }
        
        public async Task<User> Register(User user)
        {
            // Хешуємо пароль
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

            // Додаємо користувача до бази даних і зберігаємо зміни
            await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();
            return user;
        }
        
        public async Task<bool> UserExists(string username, string email)
        {
            // Перевірка на дублікати електронних адрес і імен користувачів
            return await _dataContext.Users.AnyAsync(u => u.Username == username || u.Email == email);
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            { return null;
            }
            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)) { return null;
            }

            return user;
            
        }

        public async Task<User> GetUser(string username)
        {
            
                var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Username == username);
                return user;
            
        }

        public async Task<List<User>> GetUsers()
        {
                var users = await _dataContext.Users.ToListAsync();
                return users;
        }

        public async Task UpdateUser(User user)
        {
            
                _dataContext.Users.Update(user);
                await _dataContext.SaveChangesAsync();
            
        }

        public async Task DeleteUser(User user)
        {
            
            _dataContext.Users.Remove(user);
                await _dataContext.SaveChangesAsync();
            
        }
    }
}