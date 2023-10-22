using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class ItemCategory
{
    [Required] [ForeignKey("ItemID")] public int ItemId { get; set; }
    [Required] [ForeignKey("CategoryID")] public int CategoryId { get; set; }
    [Required] public Item Item { get; set; }
    [Required] public Category Category { get; set; }
}   