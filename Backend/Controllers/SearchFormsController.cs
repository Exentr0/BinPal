using Backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Backend.Controllers;

public class SearchFormsController: Controller
{
    private readonly DataContext _context;
    
    public SearchFormsController(DataContext context)
    {
        _context = context;
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
                return View(items);
            }
            else
            {
                var searchItems = await _context.Items
                    .Include(c => c.Name)
                    .Where(s => s.Name.Contains(search))
                    .ToListAsync();

                return View(searchItems);
            }
        }
        catch (Exception ex)
        {
            return View("Error");
        }
    }

}