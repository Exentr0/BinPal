using Backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

public class SortingController : Controller
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
        [FromQuery] int offset,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice,
        [FromQuery] int sorting,
        [FromQuery] decimal? minRating)
    {
        try
        {
            var query = _context.Items.AsQueryable();

            // Фільтрація за ім'ям продукту
            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(p => p.Name.Contains(searchQuery));
            }
            else
            {
                var dataContext = _context.Items
                    .Include(c => c.Name)
                    .Include(c => c.ItemCategories)
                    .Include(c => c.Price)
                    .Include(c => c.Rating)
                    .Include(c => c.RatingValue)
                    .Include(c => c.LikesAmount);
            }

            // Фільтрація за ціною
            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            // Фільтрація за мінімальним значенням рейтингу
            if (minRating.HasValue)
            {
                query = query.Where(p => p.Rating >= (double)minRating.Value);
            }

            // Сортування
            switch (sorting)
            {
                case 1: // Спадання
                    query = query.OrderByDescending(p => p.Price);
                    break;
                case 2: // Зростання
                    query = query.OrderBy(p => p.Price);
                    break;
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

    /*public async Task<IActionResult> Indexs(string search)
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
}    */
}