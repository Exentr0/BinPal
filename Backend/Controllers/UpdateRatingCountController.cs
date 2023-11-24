using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

public class UpdateRatingCountController : Controller
{
    private readonly DataContext _context;

    private UpdateRatingCountController(DataContext context)
    {
        _context = context;
    }

    [HttpGet("{id}/Rating")]
    public IActionResult UpdateRatingAmount(int id, int ratingValue)
    {
        var item = _context.Items.Find(id);
        if (item == null)
        {
            return NotFound();
        }

        item.RatingValue++;

        item.Rating = (item.Rating * (item.RatingValue - 1) + ratingValue) / item.RatingValue;

        _context.SaveChanges();

        return Json(new { success = true });
    }
}