using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Backend.Data;
using Backend.Validation;
using FluentValidation;


namespace Backend.Models;
public class User
{
    [Key] 
    public int Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
    
    public string RefreshToken { get; set; } = string.Empty;
    
    public DateTime TokenCreated { get; set; } = DateTime.Now;

    public DateTime TokenExpires { get; set; } = DateTime.Now.AddDays(1);

    public string ProfilePictureUrl { get; set; } = "https://www.example.com/path/to/resource?query_param=value#fragment";

    public string Bio { get; set; } = string.Empty;
    
    public ShoppingCart ShoppingCart { get; set; } 
    public List<PaymentMethod>? PaymentMethods { get; set; } 
    public List<Item>? PublishedPackages { get; set; }
    public List<Purchase>? Purchases { get; set; }
    public List<ItemReview>? ItemReviews { get; set; }
}

public class UserValidator : AbstractValidator<User>
{
    private readonly DataContext _context;
    public UserValidator(DataContext context)
    {
        _context = context;
        RuleFor(u => u.Id)
            .NotNull()
            .WithMessage("Id can't be null");
        When(u => u.Id != null, () =>
        {
            RuleFor(u => u.Id)
                .IsUnique(context, u => u.Id)
                .WithMessage("Id Should be unique");
        });
        
        
        RuleFor(u => u.Username)
            .NotNull()
            .WithMessage("UserName can't be null");
        When(u => u.Username != null, () =>
            {
                RuleFor(u => u.Username)
                    .IsUnique(context, u => u.Username)
                    .WithMessage("Username is already taken. Please choose a different one.")
                    .NotEmpty()
                    .MaximumLength(20)
                    .WithMessage("Username must be a maximum of 20 characters.");
            });
            

        RuleFor(u => u.Email)
            .NotNull()
            .WithMessage("Email can't be null");
        When(u => u.Email != null, () =>
            {
                RuleFor(u => u.Email)
                    .NotEmpty().WithMessage("Email can't be empty.")
                    .Matches(@"^[^@]+@[^@]+\.[a-zA-Z]{2,}$")
                    .WithMessage("Invalid email format.")
                    .IsUnique(context, u => u.Email)
                    .WithMessage("Email is already taken. Please choose a different one.");

            }); 
        
        
        RuleFor(u => u.Password)
            .NotNull()
            .WithMessage("Password can't be null");
        When(u => u.Password != null, () =>
            {
                RuleFor(u => u.Password)
                    .NotEmpty().WithMessage("Password can't be empty")
                    .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$")
                    .WithMessage("Password must contain at least 1 lowercase letter, 1 uppercase letter, 1 digit, and be at least 8 characters long.");
            });


        RuleFor(u => u.ProfilePictureUrl)
            .NotNull()
            .WithMessage("Profile picture URL cannot be null.");
        When(u => u.ProfilePictureUrl != null, () =>
        {
            RuleFor(u => u.ProfilePictureUrl)
                .Must(CustomValidators.IsValidUrl)
                .WithMessage("Profile picture URL is not a valid URL.");
        });
        
        RuleFor(u => u.Bio)
            .NotNull()
            .WithMessage("Bio can't be null");
        When(u => u.Bio != null, () =>
            {
                RuleFor(u => u.Bio)
                    .MaximumLength(500)
                    .WithMessage("Bio cannot exceed 500 characters.");
            });
        
        RuleFor(u => u.TokenCreated)
            .NotNull()
            .WithMessage("TokenCreated cannot be null.");

        RuleFor(u => u.TokenExpires)
            .NotNull()
            .WithMessage("TokenExpires cannot be null.");
        When(u => u.TokenExpires != null, () =>
        {
            RuleFor(u => u.TokenExpires)
                .Must((user, tokenExpires) => tokenExpires > DateTime.UtcNow && tokenExpires > user.TokenCreated)
                .WithMessage("TokenExpires must be a date in the future and greater than TokenCreated.");
        });


        RuleFor(u => u.RefreshToken)
            .NotNull()
            .WithMessage("RefreshToken cannot be null");
    }
}