using System.ComponentModel.DataAnnotations.Schema;
using Backend.Data;
using FluentValidation;

namespace Backend.Models;

public class ItemPlugin
{
    [ForeignKey("ItemID")]
    public int ItemId { get; set; }

    [ForeignKey("PluginId")] 
    public int PluginId { get; set; }
    
    public Item Item { get; set; }
    
    public Plugin Plugin { get; set; }
}

public class ItemPluginValidator : AbstractValidator<ItemPlugin>
{
    public ItemPluginValidator()
    {
        RuleFor(sp => sp.PluginId)
            .NotNull()
            .WithMessage("PluginId can't be null");;

        RuleFor(sp => sp.ItemId)
            .NotNull()
            .WithMessage("ItemId can't be null");;
    }
}