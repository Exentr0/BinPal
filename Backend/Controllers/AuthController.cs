using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Backend.Models;
using Backend.Registration___Authorization;
using Backend.Services;
using Microsoft.IdentityModel.Tokens;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private static User _user = new User();
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthController(IConfiguration configuration, IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet, Authorize]
        public ActionResult<string> GetMyName()
        {
            return Ok(_userService.GetMyName());
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDtoRegister request, [FromServices] IValidator<User> validator)
        {
            var newUser = new User
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password
            };
            

            // Validate the new user using FluentValidation
            ValidationResult validationResult = await validator.ValidateAsync(newUser);

            // Check if validation failed
            if (!validationResult.IsValid)
            {
                // Convert validation errors to ModelState errors
                var modelStateDictionary = new ModelStateDictionary();
                foreach (FluentValidation.Results.ValidationFailure failure in validationResult.Errors)
                {
                    modelStateDictionary.AddModelError(
                        failure.PropertyName,
                        failure.ErrorMessage);
                }

                // Return validation errors
                return ValidationProblem(modelStateDictionary);
            }
            else
            {
                newUser.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
                // If validation succeeded, register the new user
                await _userService.Register(newUser);   
            }
            // Return the newly registered user
            return Ok(newUser);
        }


        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(UserDtoLogin request)
        {
            var loggedInUser = await _userService.Login(request.Email, request.Password);
            if (loggedInUser == null)
            {
                return new ObjectResult("User not found or wrong password")
                {
                    StatusCode = 401 
                };
            }

            string token = await CreateToken(loggedInUser);

            var refreshToken = await GenerateRefreshToken(); 
            await SetRefreshToken(refreshToken);
            
            var response = new
            {
                accessToken = token,
                refreshToken = refreshToken.Token
            };
            
            return Ok(response);
        }
        
        
        
        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (!_user.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refresh Token");
            }
            else if (_user.TokenExpires < DateTime.Now)
            {
                return Unauthorized("Token expired.");
            }

            string token = await CreateToken(_user);
            var newRefreshToken = await GenerateRefreshToken();
            await SetRefreshToken(newRefreshToken);

            return Ok(token);
        }

        private async Task<RefreshToken> GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };

            return refreshToken;
        }

        private async Task SetRefreshToken(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.ExpiresAt,
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            _user.RefreshToken = newRefreshToken.Token;
            _user.TokenCreated = newRefreshToken.CreatedAt;
            _user.TokenExpires = newRefreshToken.ExpiresAt;
        }
                
        
        private async Task<string> CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "User")
            };

            var key = GenerateKey(64); 

            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private byte[] GenerateKey(int size)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] key = new byte[size];
                rng.GetBytes(key);
                return key;
            }
        }

    }
}