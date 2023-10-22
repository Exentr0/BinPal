using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class Cart
{
    [Key] public int Id { get; set; }
    [Required] [ForeignKey("UserID")] public int UserId { get; set; }
    [Required] public User User { get; set; }
    [ForeignKey("CartItemID")] public int CartItemID { get; set; }
    public List<CartItem>? CartItems { get; set; }
}   