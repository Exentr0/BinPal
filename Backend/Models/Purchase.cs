using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class Purchase
{
    [Key] 
    public int Id { get; set; }
    
    [ForeignKey("UserId")]
    public int UserId { get; set; }
    
    [ForeignKey("ItemId")]
    public int ItemId { get; set; }
    
    public DateTime PurchaseDate { get; set; }
    
    public User User { get; set; }
    
    public Item Item { get; set; }
}