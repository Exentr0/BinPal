using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Backend.Data;
using Backend.Validation;
using FluentValidation;

namespace Backend.Models
{
    public class PaymentMethod
    {
        [Key]
        public int Id { get; set; }

        public bool IsDefault { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }

        public User User { get; set; }

        [ForeignKey("PaymentInfoId")]
        public int PaymentInfoId { get; set; }

        public PaymentDetails PaymentDetails { get; set; }
    }

    public class PaymentMethodValidator : AbstractValidator<PaymentMethod>
    {
        private readonly DataContext context;

        public PaymentMethodValidator(DataContext _context)
        {
            context = _context;

            RuleFor(pm => pm.Id)
                .NotNull()
                .WithMessage("Id can't be null");

            When(pm => pm.Id != null, () =>
            {
                RuleFor(pm => pm.Id)
                    .IsUnique(context, pm => pm.Id)
                    .WithMessage("Id should be unique");
            });

            RuleFor(pm => pm.IsDefault)
                .NotNull()
                .WithMessage("IsDefault can't be null");

            RuleFor(pm => pm.UserId)
                .NotNull()
                .WithMessage("UserId can't be null");

            RuleFor(pm => pm.PaymentInfoId)
                .NotNull()
                .WithMessage("PaymentInfoId can't be null");

            // Custom validation rule to check if a user has more payment methods with the same PaymentInfoId
            RuleFor(pm => pm.PaymentInfoId)
                .Must((paymentMethod, paymentInfoId) =>
                {
                    // Check if there are other payment methods with the same PaymentInfoId for the same user
                    return !context.UserPaymentMethods.Any(pm => pm.UserId == paymentMethod.UserId && pm.PaymentInfoId == paymentInfoId);
                })
                .WithMessage("A user can't have multiple payment methods with the same PaymentInfoId");
        }
    }

}