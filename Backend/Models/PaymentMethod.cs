using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class PaymentMethod
{
    [Key]
    public int Id { get; set; }
    
    public bool IsDefault { get; set; }
    
    [ForeignKey("UserId")] 
    public int UserId { get; set; }
    
    public User User { get; set; }
    
    [ForeignKey("PaymentInfoId")] 
    public int PaymentInfoId { get; set; }
    
    public PaymentInfo PaymentInfo { get; set; }
}