using Microsoft.AspNetCore.Mvc;
using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Backend.Controllers;

public class GetProductController: ControllerBase
{
    private readonly DataContext _context;
    /*private List<Item> items;*/
    
    public GetProductController(DataContext context)
    {
        _context = context;
    }
    
    [HttpGet("{id}/price")]
    public IActionResult GetItemPrice(int id)
    {
        var item = _context.Items.FirstOrDefault(p => p.Id == id);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item.Price);
    }
    
    [HttpGet("{id}/title")]
    public IActionResult GetItemTitle(int id)
    {
        var item = _context.Items.FirstOrDefault(p => p.Id == id);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item.Description);
    }
    
    [HttpGet("{id}/raiting")]
    public IActionResult GetItemRaiting(int id)
    {
        var item = _context.Items.FirstOrDefault(p => p.Id == id);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item.Rating);
    }
    
    [HttpGet("{id}/seller")]
    public IActionResult GetItemNameSeller(int id)
    {
        var item = _context.Items.FirstOrDefault(p => p.Id == id);
        if (item == null)
        {
            return NotFound();
        }
        var seller = _context.Users.FirstOrDefault(p => p.Id == item.PublisherId);
        return Ok(seller?.Username);
    }
    
    [HttpGet("{id}/favorite")]
    public IActionResult GetItemFavorited(int id)
    {
        var item = _context.Items.FirstOrDefault(p => p.Id == id);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item.LikesAmount);
    }
   
    [HttpGet("{id}/Likes")]
    public IActionResult GetItemLikes(int id)
    {
        var item = _context.Items.FirstOrDefault(p => p.Id == id);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item.Likes);
    }
    
}

/*для сторінки результатів пошуку я би хотів отримати продукти в такому форматі*/