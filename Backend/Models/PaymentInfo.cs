using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class PaymentInfo
{
    [Key]
    public int Id { get; set; }
    
    public int SecurityCode { get; set; }
    
    public string Provider { get; set; }
    
    public DateTime ExpirationDate { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
}