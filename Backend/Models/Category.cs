using System.ComponentModel.DataAnnotations;
using Backend.Data;
using Backend.Validation;
using FluentValidation;

namespace Backend.Models;

public class Category
{
    [Key] 
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public List<ItemCategory>? ItemCategories { get; set; }
    public List<SoftwareCategory>? SoftwareCategories { get; set; }
}

public class CategoryValidator : AbstractValidator<Category>
{
    private readonly DataContext _context;

    public CategoryValidator(DataContext context)
    {
        _context = context;
        RuleFor(c => c.Id)
            .NotNull().WithMessage("Id can't be null");
        When(c => c.Id != null, () =>
            {
                RuleFor(c => c.Id)
                    .IsUnique(context, c => c.Id)
                        .WithMessage("Id Should be unique");
            }); 
 
        
        RuleFor(c => c.Name)
            .NotNull().WithMessage("Name can't be null");
        When(c => c.Name != null, () =>
            {
                RuleFor(c => c.Name)
                    .IsUnique(context, c => c.Name)
                         .WithMessage("Name Should be unique")
                    .MaximumLength(20)
                        .WithMessage("Name must be a maximum of 20 characters.");
            });
    }
}