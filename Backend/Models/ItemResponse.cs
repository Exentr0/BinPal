namespace Backend.Models;

public class ItemResponse
{
    public List<Item> Items { get; set; } = new List<Item>();
    public int CurrentPage { get; set; }
    public int Pages { get; set; }
}