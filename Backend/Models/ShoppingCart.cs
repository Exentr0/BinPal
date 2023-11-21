using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Backend.Data;
using Backend.Validation;
using FluentValidation;

namespace Backend.Models
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        
        public User User { get; set; }

        public List<CartItem>? CartItems { get; set; }
    }

    public class ShoppingCartValidator : AbstractValidator<ShoppingCart>
    {
        private readonly DataContext _context;

        public ShoppingCartValidator(DataContext context)
        {
            _context = context;

            RuleFor(sc => sc.Id)
                .NotNull()
                .WithMessage("Id can't be null");
            When(sc => sc.Id != null, () =>
            {
                RuleFor(sc => sc.Id)
                    .IsUnique(_context, sc => sc.Id)
                    .WithMessage("Id should be unique");
            });

            RuleFor(sc => sc.UserId)
                .NotNull()
                .WithMessage("UserId can't be null");
        }
    }
}