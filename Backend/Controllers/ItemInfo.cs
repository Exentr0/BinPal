using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Backend.Data;
using Backend.Models;
using Backend.Services;
namespace Backend.Controllers;
using Backend.Services.Storage;
using System.Linq;
[Route("api/items")]
[ApiController]
public class ItemController : ControllerBase
{
    private readonly DataContext _context;
    private readonly ItemPicturesBlobService _itemPicturesBlobService;
    public ItemController(DataContext context, ItemPicturesBlobService itemPicturesBlobService)
    {
        _context = context;
        _itemPicturesBlobService = itemPicturesBlobService;
    }

    [HttpGet("{id}")]
    public IActionResult GetItem(int id)
    {
        var item = _context.Items.FirstOrDefault(i => i.Id == id);
        if (item == null)
        {
            return NotFound();
        }

        var owner = _context.Users.FirstOrDefault(u => u.Id == item.PublisherId);
        var items = _context.Items
            .Where(i => i.PublisherId == owner.Id && i.Id != id) // Exclude the current item
            .Take(4)
            .ToList(); // Execute the query to get the items

        var itemPictureUrls = _itemPicturesBlobService.GetItemPictureUrlsAsync(id);

        var result1 = new
        {
            Id = id,
            Name = item.Name,
            Price = item.Price,
            Owner = owner.Username,
            Rating = item.Rating,
            Overview = item.Description,
            PublisherInfo = item.PublisherInfo,
            License = item.License,
            Pictures = itemPictureUrls,
            RelatedItems = items.Select(i => new
            {
                Id = i.Id,
                Name = i.Name,
                Price = i.Price,
                Rating = i.Rating,
                Overview = i.Description,
                // Add other properties as needed
            }).ToList(),
        };
        
        
        return new JsonResult(result1);
    }
}