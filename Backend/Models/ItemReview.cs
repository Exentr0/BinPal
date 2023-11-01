using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class ItemReview
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
    [Range(0, 5)]
    public int Rating { get; set; }
    
    [MaxLength(500)] 
    public string? Comment { get; set; }
    
    [Required]
    public DateTime Time { get; set; }
    
    [Required]
    public User User { get; set; }
    
    [Required]
    public Item Item { get; set; }
}