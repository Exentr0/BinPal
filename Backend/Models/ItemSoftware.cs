using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;

namespace Backend.Models;

public class ItemSoftware
{
    [ForeignKey("ItemId")] 
    public int ItemId { get; set; }

    [ForeignKey("SoftwareId")] 
    public int SoftwareId { get; set; }

    public Item Item { get; set; }

    public Software Software { get; set; }
}

public class SoftwareItemValidator : AbstractValidator<ItemSoftware>
{
    public SoftwareItemValidator()
    {
        RuleFor(its => its.ItemId)
            .NotNull()
            .WithMessage("ItemId can't be null");
        
        RuleFor(its => its.SoftwareId)
            .NotNull()
            .WithMessage("SoftwareId can't be null");
    }
}