using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class Coupon
{
    [Key]
    public int Id { get; set; }
    public int Staff_id { get; set; }
    [ForeignKey("Staff")]
    public string Coupon_description { get;set;}
    public int Discount_value { get; set; }
    public string Discount_type { get; set; }
    public int Times_used { get; set; }
    public int Max_used { get; set; }
    public DateTime Coupon_start_date { get; set; }
    public DateTime Coupon_end_date { get; set; }
    public DateTime Created_at { get; set; }
}