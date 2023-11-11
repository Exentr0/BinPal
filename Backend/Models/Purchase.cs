using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Backend.Data;
using Backend.Validation;
using FluentValidation;

namespace Backend.Models
{
    public class Purchase 
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }

        [ForeignKey("ItemId")]
        public int ItemId { get; set; }

        public DateTime PurchaseDate { get; set; }

        public User User { get; set; }

        public Item Item { get; set; }
    }

    public class PurchaseValidator : AbstractValidator<Purchase>
    {
        private readonly DataContext _context;

        public PurchaseValidator(DataContext context)
        {
            _context = context;

            RuleFor(p => p.Id)
                .NotNull().WithMessage("Id can't be null");
            When(p => p.Id != null, () =>
            {
                RuleFor(p => p.Id)
                    .IsUnique(_context, p => p.Id)
                    .WithMessage("Id should be unique");
            });

            RuleFor(p => p.PurchaseDate)
                .NotNull().WithMessage("Purchase Date can't be null");
            When(p => p.PurchaseDate != null, () =>
            {
                RuleFor(p => p.PurchaseDate)
                    .Must(CustomValidators.IsFutureDate)
                    .WithMessage("Purchase Date must be in the future.");
            });

            RuleFor(p => p.UserId)
                .NotNull().WithMessage("UserId can't be null");

            RuleFor(p => p.ItemId)
                .NotNull().WithMessage("ItemId can't be null");
        }
    }
}