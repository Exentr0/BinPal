using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class Payment
{
    [Key] 
    public int Id { get; set; }
    public int Customer_id { get; set; }
    [ForeignKey("Customer")]
    public int Seller_id { get; set; }
    [ForeignKey("Seller")] 
    public int Order_id { get; set; }
    [ForeignKey("Order")] 
    public bool Allowed { get; set; }

    public int Card_number { get; set; }
    public DateTime Card_end_date { get; set; }
}