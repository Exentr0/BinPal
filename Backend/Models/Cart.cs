using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class Cart
{
    [Key] 
    public int Id { get; set; }
    
    [Required]
    [ForeignKey("UserId")] 
    public int UserId { get; set; }
    
    [Required] 
    public User User { get; set; }
    
    [ForeignKey("CartItemId")]
    public int? CartItemId { get; set; }
    
    public List<CartItem>? CartItems { get; set; }
}   