using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class ShoppingCart
{
    [Key] 
    public int Id { get; set; }
    
    [ForeignKey("UserId")] 
    public int UserId { get; set; }
    
    public User User { get; set; }
    
    [ForeignKey("CartItemId")]
    public int? CartItemId { get; set; }
    
    public List<CartItem>? CartItems { get; set; }
}   