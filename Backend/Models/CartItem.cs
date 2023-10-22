using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class CartItem
{
    [Required] [ForeignKey("ItemID")] public int ItemId { get; set; }
    [Required] [ForeignKey("CartID")] public int CartId { get; set; }
    [Required] public Cart Cart { get; set; }
    [Required] public Item Item { get; set; }
}