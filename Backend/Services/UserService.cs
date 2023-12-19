using System.Security.Claims;
using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
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
            
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

            if (!string.IsNullOrEmpty(userIdClaim))
            {
                var user = _dataContext.Users.Find(int.Parse(userIdClaim));

                if (user != null)
                {
                    string username = user.Username;

                    if (!string.IsNullOrEmpty(username))
                    {
                        result = username;
                    }
                }
            }
            
            return result;
        }

        public int GetUserIdFromToken()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == "Id");

            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
            {
                return userId;
            }
            else
            {
                // Вивести відладкові повідомлення, щоб дізнатися, чому не вдалося отримати ідентифікатор користувача
                if (userIdClaim == null)
                {
                    Console.WriteLine("Claim 'Id' not found in user claims.");
                }
                else
                {
                    Console.WriteLine("Failed to parse 'Id' claim value.");
                }

                return -1;
            }
        }
        
        public async Task<User> Register(User user)
        {
            // Додаємо користувача до бази даних і зберігаємо зміни
            await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();
            return user;
        }
        

        public async Task<User> Login(string email, string password)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                // Повертаємо null, якщо користувача з вказаним email не знайдено.
                return null;
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                // Повертаємо null, якщо пароль не співпадає.
                return null;
            }

            // Якщо користувач і пароль відповідають, повертаємо об'єкт користувача.
            return user;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new InvalidOperationException($"User with ID {userId} not found.");
            }
            return user;
        }

        
        public async Task<User> GetUserByName(string username)
        {
            
                var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Username == username);
                if (user == null)
                {
                    throw new InvalidOperationException($"User with Username {username} not found.");
                }
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