using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class CartItem
{
    [ForeignKey("ItemId")] 
    public int ItemId { get; set; }
    
    [ForeignKey("CartId")] 
    public int CartId { get; set; }
    
    public Cart Cart { get; set; }
    
    public Item Item { get; set; }
}