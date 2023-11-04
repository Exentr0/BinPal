using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class Item
{
    [Key]
    public int Id { get; set; }
    
 
    public string Name { get; set; }
    

    [RegularExpression(@"^\d+(\.\d{1,2})?$")]
    public float Rating { get; set; } 
    public int LikesAmount { get; set; }
    
    public int Price { get; set; }
    
    public string? Description { get; set; }
    
    public string? PublisherInfo { get; set; }
    
    public string License { get; set; }
    
    [ForeignKey("PublisherId")] 
    public int PublisherId { get; set; }
    
    public User User { get; set; }
    
    public List<CartItem>? CartItems { get; set; }
    public List<Purchase>? Purchases { get; set; }
    public List<ItemReview>? Reviews { get; set; }
    public List<ItemCategory>? ItemCategories { get; set; }
}