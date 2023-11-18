using System.ComponentModel.DataAnnotations;
using Backend.Data;
using Backend.Validation;
using FluentValidation;

namespace Backend.Models;

public class PaymentInfo
{
    [Key]
    public int Id { get; set; }
    
    public string SecurityCode { get; set; }
    
    public string Provider { get; set; }
    
    public DateTime ExpirationDate { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
}

public class PaymentInfoValidator : AbstractValidator<PaymentInfo>
{
    private readonly DataContext context;
    public PaymentInfoValidator(DataContext _context)
    {
        context = _context;
        
        RuleFor(pi => pi.Id)
            .NotNull().WithMessage("Id can't be null");
        When(pi => pi.Id != null, () =>
        {
            RuleFor(pi => pi.Id)
                .IsUnique(context, pi => pi.Id)
                .WithMessage("Id Should be unique");
        });
        
        RuleFor(pi => pi.SecurityCode)
            .NotNull().WithMessage("Security Code can't be null");
        When(pi => pi.SecurityCode != null, () =>
        {
            RuleFor(pi => pi.SecurityCode)
                .Matches("^[0-9]{3}$")
                .WithMessage("Security Code must be a 3-digit number.");
        });
        
        RuleFor(pi => pi.Provider)
            .NotNull().WithMessage("Provider can't be null");
        When(pi => !string.IsNullOrWhiteSpace(pi.Provider), () =>
        {
            RuleFor(pi => pi.Provider)
                .Must(IsAllowedProvider)
                .WithMessage("Provider must be Visa, Mastercard, or Paypal.");
        });
        
        RuleFor(pi => pi.ExpirationDate)
            .NotNull().WithMessage("Expiration Date can't be null");
        When(pi => pi.ExpirationDate != null, () =>
        {
            RuleFor(pi => pi.ExpirationDate)
                .Must(CustomValidators.IsFutureDate)
                .WithMessage("Expiration Date must be in the future.");
        });

    }
    
    private bool IsAllowedProvider(string provider)
    {
        return provider.Equals("Visa", StringComparison.OrdinalIgnoreCase)
               || provider.Equals("Mastercard", StringComparison.OrdinalIgnoreCase)
               || provider.Equals("Paypal", StringComparison.OrdinalIgnoreCase);
    }
}