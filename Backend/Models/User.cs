using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class User
{
    [Key] 
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime TokenCreated { get; set; }
    public DateTime TokenExpires { get; set; }
    public string ProfilePicture { get; set; } = "default-profile-picture.jpg";
    
    public string? Bio { get; set; }
    public ShopingCart ShopingCart { get; set; } 
    public List<PaymentMethod>? PaymentMethods { get; set; } 
    public List<Item>? PublishedPackages { get; set; }
    public List<Purchase>? Purchases { get; set; }
    public List<ItemReview>? ItemReviews { get; set; }  
}