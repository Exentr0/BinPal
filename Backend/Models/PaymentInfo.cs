using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class PaymentInfo
{
    [Key] public int Id { get; set; }
    [Range(0, 999)]
    [Required] public int SecurityCode { get; set; }
    [Required] public string Provider { get; set; }
    [Required] public DateTime ExpirationDate { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
}