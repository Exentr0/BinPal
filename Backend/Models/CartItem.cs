using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class CartItem
{
    [ForeignKey("ItemId")] 
    public int ItemId { get; set; }
    
    [ForeignKey("CartId")] 
    public int CartId { get; set; }
    
    public int Quantity { get; set; }

    public System.DateTime DateCreated { get; set; }
    public int ProductId { get; set; }
    public ShopingCart ShopingCart { get; set; }
    public Item Item { get; set; }
}