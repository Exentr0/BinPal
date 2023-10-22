using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class Order
{
    [Key] 
    public int Id { get; set; }
    public int Coupon_id { get; set; }
    [ForeignKey("Coupon")]
    public int Customer_id { get; set; }
    [ForeignKey("Customer")]
    public DateTime Created_at { get; set; }
    public int Total_price { get; set; }
}