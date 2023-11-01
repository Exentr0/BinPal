using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class Purchase
{
    [Key] 
    public int Id { get; set; }
    
    [Required]
    [ForeignKey("UserId")]
    public int UserId { get; set; }
    
    [Required]
    [ForeignKey("ItemId")]
    public int ItemId { get; set; }
    
    [Required] 
    public DateTime PurchaseDate { get; set; }
    
    [Required] 
    public User User { get; set; }
    
    [Required]
    public Item Item { get; set; }
}