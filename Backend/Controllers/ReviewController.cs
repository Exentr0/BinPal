using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Backend.Data;
using Backend.Models;

namespace Backend.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ItemReviewController : ControllerBase
    {
        private readonly DataContext _context;

        public ItemReviewController(DataContext context)
        {
            _context = context;
        }

        // Отримати відгуки для певного продукта за його Id
        [HttpGet("{id}/reviews")]
        public IActionResult GetItemReviews(int id)
        {
            var itemReviews = _context.ItemReviews.Where(r => r.ItemId == id)
                .Select(review => new
                {
                    UserName = _context.Users.FirstOrDefault(u => u.Id == review.UserId).Username,
                    Stars = review.Rating,
                    Comment = review.Comment
                }).ToList();

            if (itemReviews.Count == 0)
            {
                return NotFound("No reviews found for this item.");
            }

            return new JsonResult(itemReviews);
        }
    }
}