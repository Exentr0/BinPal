using Backend.Data;
using FluentValidation;

namespace Backend.Models;

public class UserTest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class UserTestValidator : AbstractValidator<UserTest>
{
    private readonly DataContext _context;
    public UserTestValidator(DataContext context)
    {
        _context = context;

        RuleFor(userTest => userTest.Name)
            .Must(IsUniqueUserName)
            .WithMessage("Name is already taken. Please choose a different one.")
            .NotNull()
            .NotEmpty()
            .MaximumLength(20)
            .WithMessage("Name is required and must be a maximum of 20 characters.");
    }

    private bool IsUniqueUserName(string Name)
    {
        bool isUnique = !_context.UserTest.Any(u => u.Name == Name);
        return isUnique;
    }
}