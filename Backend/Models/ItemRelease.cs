using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Backend.Data;
using Backend.Validation;
using FluentValidation;

namespace Backend.Models;

public class ItemRelease
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    [ForeignKey("ItemId")]
    public int ItemId { get; set; }

    public Item Item { get; set; }
}

public class ItemReleaseValidator : AbstractValidator<ItemRelease>
{
    private readonly DataContext _context;
    public ItemReleaseValidator(DataContext context)
    {
        _context = context;
        RuleFor(ir => ir.Id)
            .NotNull().WithMessage("Id can't be null");
        When(ir => ir.Id != null, () =>
        {
            RuleFor(ir => ir.Id)
                .IsUnique(_context, ir => ir.Id)
                .WithMessage("Id should be unique");
        });

        RuleFor(ir => ir.Name)
            .NotNull().WithMessage("Name can't be null");
        When(ir => ir.Name != null, () =>
        {
            RuleFor(ir => ir.Name)
                .NotEmpty()
                .WithMessage("Name can't be empty.")
                .MaximumLength(40)
                .WithMessage("Name must be a maximum of 40 characters.");
        });

        RuleFor(ir => ir.Description)
            .NotNull().WithMessage("Description can't be null");
        When(ir => ir.Description != null, () =>
        {
            RuleFor(ir => ir.Name)
                .NotEmpty()
                .WithMessage("Description can't be empty.")
                .MaximumLength(200)
                .WithMessage("Description must be a maximum of 200 characters.");
        });
    }
}