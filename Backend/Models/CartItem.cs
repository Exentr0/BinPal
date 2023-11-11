using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Backend.Data;
using FluentValidation;

namespace Backend.Models;

public class CartItem
{
    [ForeignKey("ItemId")] 
    public int ItemId { get; set; }
    
    [ForeignKey("CartId")] 
    public int CartId { get; set; }
    
    public ShoppingCart ShoppingCart { get; set; }
    
    public Item Item { get; set; }
}

public class CartItemValidator : AbstractValidator<CartItem>
{
    public CartItemValidator()
    {
        RuleFor(ca => ca.ItemId)
            .NotNull()
            .WithMessage("itemId can't be null");

        RuleFor(ca => ca.CartId)
            .NotNull()
            .WithMessage("CartId can't be null");;
    }
}