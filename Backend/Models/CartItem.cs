using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class CartItem
{
    [Required]
    [ForeignKey("ItemId")] 
    public int ItemId { get; set; }
    
    [Required]
    [ForeignKey("CartId")] 
    public int CartId { get; set; }
    
    [Required]
    public Cart Cart { get; set; }
    
    [Required]
    public Item Item { get; set; }
}