using Backend.Data;
using Backend.Models;
using Backend.Services.Storage;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

public class SortingController : Controller
{
    private readonly DataContext _context;
    private readonly ItemPicturesBlobService _itemPicturesBlobService;
    public SortingController(DataContext context,ItemPicturesBlobService itemPicturesBlobService)
    {
        _itemPicturesBlobService = itemPicturesBlobService;
        _context = context;
    }

    [HttpPost("/api/Sorting")]
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
            query = query.Where(p => p.Price >= Math.Floor(model.MinPrice.Value));
        }

        if (model.MaxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= Math.Floor(model.MaxPrice.Value));
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

        query = query.Include(p => p.User);

        // Повернення результатів
        var items = await query
            .Select(p => new
            {
                p.Id,
                p.Name,
                p.Rating,
                p.LikesAmount,
                p.Price,
                p.Description,
                p.PublisherInfo,
                p.License,
                p.PublisherId,
                p.User.Username,
                Images = _itemPicturesBlobService.GetItemPictureUrlsAsync(p.Id).Result,
            })
            .ToListAsync();

        var totalCount = await query.CountAsync();


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

    [HttpGet("/api/item")]
    public async Task<ActionResult<object>> GetProducts([FromQuery] int page = 1, [FromQuery] int limit = 24)
    {
        try
        {
            if (_context.Items == null)
            {
                return NotFound();
            }

            var pageResults = (float)limit;
            var pageCount = Math.Ceiling(_context.Items.Count() / pageResults);
            var products = await _context.Items
                .Skip((page - 1) * limit).Take(limit).ToListAsync();

            var response = new
            {
                TotalPages = (int)pageCount,
                CurrentPage = page,
                Items = products
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
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