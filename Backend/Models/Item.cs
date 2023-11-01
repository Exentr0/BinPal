using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class Item
{
    [Key]
    public int Id { get; set; }
    
    [MaxLength(50)] 
    [Required] 
    public string Name { get; set; }
    
    [Range(0, 5)] 
    [RegularExpression(@"^\d+(\.\d{1,2})?$")]
    public float Rating { get; set; } //2 digits after coma
    public int LikesAmount { get; set; }
    
    [Required] 
    public int Price { get; set; }
    
    [MaxLength(1000)]
    public string? Description { get; set; }
    
    [MaxLength(500)]
    public string? PublisherInfo { get; set; }
    
    [Required]
    [MaxLength(500)]
    public string License { get; set; }
    
    [Required] 
    [ForeignKey("PublisherId")] 
    public int PublisherId { get; set; }
    
    [Required] 
    public User User { get; set; }
    
    public List<CartItem>? CartItems { get; set; }
    public List<Purchase>? Purchases { get; set; }
    public List<ItemReview>? Reviews { get; set; }
    public List<ItemCategory>? ItemCategories { get; set; }
}