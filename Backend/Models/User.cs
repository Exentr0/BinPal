using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;
public class User
{
    [Key] public int Id { get; set; }
    [Required] [MaxLength(50)] public string Username { get; set; }
    [Required] [MaxLength(100)] public string Email { get; set; }
    [Required] public string Password { get; set; }
    [Required] public byte[] ProfilePicture { get; set; }
    [MaxLength(255)] public string? Bio { get; set; }
    [Required] public Cart Cart { get; set; } 
    public List<PaymentMethod>? PaymentMethods { get; set; } 
    public List<Item>? PublishedPackages { get; set; }
    public List<Purchase>? Purchases { get; set; }
    public List<ItemReview>? ItemReviews { get; set; }
}