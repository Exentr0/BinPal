using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Backend.Models;

public class Product
{
    // Якщо тут десь терба додати FK, то додайте 
    [Key]
    public int id { get; set; }
    public int Seller_id { get; set; }
    public int Galaty_image_id { get; set; }
    public string Name { get; set; }
    public int Regular_price { get; set; }
    public int Discount_price { get; set; }
    public string Short_description { get; set; }
    public string Description { get; set; }
    public DateTime Created_at { get; set; }
    public string Platform_name { get; set; }
    public byte[] Main_name { get; set; }
}