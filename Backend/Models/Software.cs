using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Backend.Data;
using Backend.Validation;
using FluentValidation;

namespace Backend.Models
{
    public class Software
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string PictureUrl { get; set; }

        public List<SoftwareCategory> SoftwareCategories { get; set; }
    }

    public class SoftwareValidator : AbstractValidator<Software>
    {
        private readonly DataContext _context;

        public SoftwareValidator(DataContext context)
        {
            _context = context;

            RuleFor(s => s.Id)
                .NotNull().WithMessage("ID can't be null");
            When(s => s.Id != null, () =>
            {
                RuleFor(s => s.Id)
                    .IsUnique(_context, s => s.Id).WithMessage("Id should be unique");
            });
            
            RuleFor(s => s.Name)
                .NotNull().WithMessage("Name can't be null");
            When(s => s.Name != null, () =>
            {
                RuleFor(s => s.Name)
                    .IsUnique(_context, s => s.Name).WithMessage("Name should be unique")
                    .MaximumLength(20).WithMessage("Name must be a maximum of 20 characters");
            });

            RuleFor(s => s.PictureUrl)
                .NotNull().WithMessage("Picture URL can't be null");
            When(s => s.PictureUrl != null, () =>
            {
                RuleFor(s => s.PictureUrl)
                    .Must(CustomValidators.IsValidUrl)
                    .WithMessage("Pictures URL is not a valid URL.")
                    .IsUnique(_context, s => s.PictureUrl).WithMessage("Picture URL should be unique");
            });
        }
    }
}