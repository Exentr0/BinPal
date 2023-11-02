using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class ItemCategory
{
    [ForeignKey("ItemId")]
    public int ItemId { get; set; }
    
    [ForeignKey("CategoryId")] 
    public int CategoryId { get; set; }
    
    public Item Item { get; set; }
    
    public Category Category { get; set; }
}   