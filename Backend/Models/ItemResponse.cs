namespace Backend.Models;

public class ItemResponse
{
    public List<Item> Items { get; set; } = new List<Item>();
    public int currentPage { get; set; }
    public int page { get; set; }
}