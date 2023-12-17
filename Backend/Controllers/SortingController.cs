using Backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

public class SortingController : ControllerBase
{
    private readonly DataContext _context;

    public SortingController(DataContext context)
    {
        _context = context;
    }

    [HttpGet("item/sorting")]
    public async Task<ActionResult<object>> SortingProducts(
        [FromQuery] string searchQuery,
        [FromQuery] int limit,
        [FromQuery] int offset)
    {
        try
        {
            var query = _context.Items.AsQueryable();

            // Фільтрація за ім'ям продукту
            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(p => p.Name.Contains(searchQuery));
            }

            int totalCount = await query.CountAsync();

            // Сортування, пагінація та отримання товарів
            var items = await query.Skip(offset).Take(limit).ToListAsync();

            var result = new
            {
                TotalCount = totalCount,
                Products = items
            };

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }


    [Route("item/{min}/{max}")]
    public IActionResult SortingSlider(decimal min, decimal max)
    {
        var item = _context.Items.Where(p => p.Price >= min && p.Price <= max);
        return new JsonResult(item);
    }

    public async Task<IActionResult> Index(string search)
    {
        try
        {
            if (string.IsNullOrEmpty(search))
            {
                var dataContext = _context.Items
                    .Include(c => c.Name)
                    .Include(c => c.ItemCategories)
                    .Include(c => c.Price)
                    .Include(c => c.Rating)
                    .Include(c => c.RatingValue)
                    .Include(c => c.Likes)
                    .Include(c => c.LikesAmount);

                var items = await dataContext.ToListAsync();
                return new JsonResult(items);
            }
            else
            {
                var searchItems = await _context.Items
                    .Include(c => c.Name)
                    .Where(s => s.Name.Contains(search))
                    .ToListAsync();

                return new JsonResult(searchItems);
            }
        }
        catch (Exception ex)
        {
            return BadRequest($"Error");
        }
    }
}    