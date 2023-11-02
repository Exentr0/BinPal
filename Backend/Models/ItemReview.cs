using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class ItemReview
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("UserId")]
    public int UserId { get; set; }
    
    [ForeignKey("ItemId")]
    public int ItemId { get; set; }
    
    public int Rating { get; set; }
    
    public string? Comment { get; set; }
    
    public DateTime Time { get; set; }
    
    public User User { get; set; }
    
    public Item Item { get; set; }
}