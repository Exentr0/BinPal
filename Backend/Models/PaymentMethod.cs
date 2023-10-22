using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class PaymentMethod
{
    [Key] public int Id { get; set; }
    [Required] public bool IsDefault { get; set; }
    [Required] [ForeignKey("UserID")] public int UserId { get; set; }
    [Required] public User User { get; set; }
    [Required] [ForeignKey("PaymentInfoID")] public int PaymentInfoId { get; set; }
    [Required] public PaymentInfo PaymentInfo { get; set; }
}