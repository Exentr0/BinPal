using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Backend.Data;
using Backend.Models;
using Backend.Services;
namespace Backend.Controllers;
using Backend.Services.Storage;
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
        var itemPictureUrls = _itemPicturesBlobService.GetItemPictureUrlsAsync(id);
        var result = new
        {
            Id = id,
            Name = item.Name,
            Price = item.Price,
            Owner = owner.Username,
            Rating = item.Rating,
            Overview = item.Description,
            PublisherInfo = item.PublisherInfo,
            License = item.License, 
            Pictures = itemPictureUrls
        };

        return new JsonResult(result);
    }
}