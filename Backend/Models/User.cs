using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;
public class User
{
    [Key] 
    public int Id { get; set; }
    
    public string Username { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
    
    public byte[] ProfilePicture { get; set; }
    
    public string? Bio { get; set; }
    
    public ShopingCart ShopingCart { get; set; } 
    public List<PaymentMethod>? PaymentMethods { get; set; } 
    public List<Item>? PublishedPackages { get; set; }
    public List<Purchase>? Purchases { get; set; }
    public List<ItemReview>? ItemReviews { get; set; }
}