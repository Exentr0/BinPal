using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.HttpLogging;
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

    [HttpPost("item/sorting")]
    public async Task<ActionResult<object>> SortingProducts([FromBody] SortingRequestModel model)
    {
        try
        {
            var query = _context.Items.AsQueryable();

            // Фільтрація за ім'ям продукту
            if (!string.IsNullOrEmpty(model.SearchQuery))
            {
                query = query.Where(p => p.Name.Contains(model.SearchQuery));
            }

            // Фільтрація за ціною
            if (model.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= model.MinPrice.Value);
            }

            if (model.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= model.MaxPrice.Value);
            }

            // Фільтрація за мінімальним значенням рейтингу
            if (model.MinRating.HasValue)
            {
                query = query.Where(p => p.Rating >= (double)model.MinRating.Value);
            }
            
            // Сортування
            switch (model.Sorting)
            {
                case 1: // Спадання
                    query = query.OrderByDescending(p => p.Price);
                    break;
                case 2: // Зростання
                    query = query.OrderBy(p => p.Price);
                    break;
            }
            var totalCount = await query.CountAsync();
            var items = await query.ToListAsync();

            return new
            {
                TotalCount = totalCount,
                Products = items
            };
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    [HttpGet("/{page}")]
    public async Task<ActionResult<List<Item>>> GetProduct(int page)
    {
        if (_context.Items == null)
        {
            return NotFound();
        }

        var pageResults = 3f;
        var pageCount = Math.Ceiling(_context.Items.Count() / pageResults);
        var products = await _context.Items
            .Skip((page - 1) * (int)pageResults).Take((int)pageResults).ToListAsync();

        var response = new ItemResponse
        {
            Items = products,
            CurrentPage = page,
            Pages = (int)pageCount
        };

        return Ok(response);
    }

    public class SortingRequestModel
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
        public string SearchQuery { get; set; }
        public string ReleaseDate { get; set; }
        public int Sorting { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public decimal? MinRating { get; set; }
    }
}