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

        public List<SoftwareCategory>? SoftwareCategories { get; set; }
        public List<Plugin>? SoftwarePlugins { get; set; }
        public List<ItemSoftware>? SoftwareItem { get; set; }
    }

    public class SoftwareValidator : AbstractValidator<Software>
    {
        private readonly DataContext _context;

        public SoftwareValidator(DataContext context)
        {
            _context = context;

            RuleFor(s => s.Id)
                .NotNull()
                .WithMessage("ID can't be null");
            When(s => s.Id != null, () =>
            {
                RuleFor(s => s.Id)
                    .IsUnique(_context, s => s.Id)
                    .WithMessage("Id should be unique");
            });
            
            RuleFor(s => s.Name)
                .NotNull()
                .WithMessage("Name can't be null");
            When(s => s.Name != null, () =>
            {
                RuleFor(s => s.Name)
                    .IsUnique(_context, s => s.Name)
                    .WithMessage("Name should be unique")
                    .MaximumLength(20).WithMessage("Name must be a maximum of 20 characters");
            });
        }
    }
}