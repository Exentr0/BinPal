using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Backend.Data;
using Backend.Validation;
using FluentValidation;

namespace Backend.Models;

public class ItemReview
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("UserId")]
    public int UserId { get; set; }
    
    [ForeignKey("ItemId")]
    public int ItemId { get; set; }
    
    public int Rating { get; set; }
    
    public string? Comment { get; set; }
    
    public DateTime Time { get; set; }
    
    public User User { get; set; }
    
    public Item Item { get; set; }
}

public class ItemReviewValidator : AbstractValidator<ItemReview>
{
    private readonly DataContext context;
    public ItemReviewValidator(DataContext _context)
    {
        context = _context;
        
        RuleFor(ir => ir.Id)
            .NotNull()
            .WithMessage("Id can't be null");
        When(ir => ir.Id != null, () =>
        {
            RuleFor(ir => ir.Id)
                .IsUnique(context, ir => ir.Id)
                .WithMessage("Id Should be unique");
        });
        
        RuleFor(ir => ir.UserId)
            .NotNull()
            .WithMessage("UserID can't be null");
        
        RuleFor(ir => ir.ItemId)
            .NotNull()
            .WithMessage("ItemId can't be null");
        
        RuleFor(ir => ir.Rating)
            .NotNull()
            .WithMessage("Rating can't be null");
        When(ir => ir.Rating != null, () =>
        {
            RuleFor(ir => ir.Rating)
                .InclusiveBetween(0, 5)
                .WithMessage("Rating must be a float value between 0 and 5.");
        });

        RuleFor(ir => ir.Comment)
            .NotNull();
        When(ir => ir.Comment != null, () =>
        {
            RuleFor(ir => ir.Comment)
                .MaximumLength(500)
                .WithMessage("Comment must be a maximum of 500 characters.");
        });
        
        RuleFor(ir => ir.Time)
            .NotNull()
            .WithMessage("Time can't be null");
    }
}