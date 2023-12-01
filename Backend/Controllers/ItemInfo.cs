using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Backend.Data;
using Backend.Models;

namespace Backend.Controllers;
[Route("api/items")]
[ApiController]
public class ItemController : ControllerBase
{
    private readonly DataContext _context;

    public ItemController(DataContext context)
    {
        _context = context;
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

        var result = new
        {
            Name = item.Name,
            Price = item.Price,
            Owner = owner.Username,
            Rating = item.Rating,
            Overview = item.Description,
            PublisherInfo = item.PublisherInfo,
            License = item.License
        };

        return new JsonResult(result);
    }
}