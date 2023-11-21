using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Backend.Data;
using FluentValidation;

namespace Backend.Models;

public class ItemCategory
{
    [ForeignKey("ItemId")]
    public int ItemId { get; set; }
    
    [ForeignKey("CategoryId")] 
    public int CategoryId { get; set; }
    
    public Item Item { get; set; }
    
    public Category Category { get; set; }
}   

public class ItemCategoryValidator : AbstractValidator<ItemCategory>
{

    public ItemCategoryValidator()
    {
        RuleFor(ic => ic.ItemId)
            .NotNull()
            .WithMessage("ItemId can't be null");
            
        RuleFor(ic => ic.CategoryId)
            .NotNull()
            .WithMessage("CategoryId can't be null");
    }
}