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
    
    public string Username { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
    
    public string ProfilePictureUrl { get; set; }
    
    public string Bio { get; set; }
    
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
            .NotNull().WithMessage("Id can't be null");
        When(u => u.Id != null, () =>
        {
            RuleFor(u => u.Id)
                .IsUnique(context, u => u.Id)
                .WithMessage("Id Should be unique");
        });
        
        
        RuleFor(u => u.Username)
            .NotNull().WithMessage("UserName can't be null");
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
            .NotNull().WithMessage("Email can't be null");
        When(u => u.Email != null, () =>
            {
                RuleFor(u => u.Email)
                    .IsUnique(context, u => u.Email)
                    .WithMessage("Email is already taken. Please choose a different one.")
                    .NotEmpty()
                    .Must(email => email.Contains("@"))
                    .WithMessage("Email must contain the @ symbol.");
            }); 
        
        
        RuleFor(u => u.Password)
            .NotNull().WithMessage("Password can't be null");
        When(u => u.Password != null, () =>
            {
                RuleFor(u => u.Password)
                    .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$")
                    .WithMessage("Password must contain at least 1 lowercase letter, 1 uppercase letter, 1 digit, and be at least 8 characters long.");
            });


        RuleFor(u => u.ProfilePictureUrl)
            .NotNull().WithMessage("Profile picture URL cannot be null.");
        When(u => u.ProfilePictureUrl != null, () =>
        {
            RuleFor(u => u.ProfilePictureUrl)
                .Must(CustomValidators.IsValidUrl)
                .WithMessage("Profile picture URL is not a valid URL.")
                .IsUnique(_context, u => u.ProfilePictureUrl).WithMessage("Profile Picture URL should be unique");;
        });
        
        RuleFor(u => u.Bio)
            .NotNull().WithMessage("Bio can't be null");
        When(u => u.Bio != null, () =>
            {
                RuleFor(u => u.Bio)
                    .MaximumLength(500)
                    .WithMessage("Bio cannot exceed 500 characters.");
            });
           
    }
}