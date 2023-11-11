using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Category
{
    [Key] 
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public List<ItemCategory>? ItemCategories { get; set; }
    public List<SoftwareCategory>? SoftwareCategories { get; set; }
}