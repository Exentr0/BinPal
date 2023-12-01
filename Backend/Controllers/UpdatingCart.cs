using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Backend.Data;
using Backend.Models;

namespace Backend.Controllers
//adding item to cart of particular user
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly DataContext _context;

        public CartController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("add-to-cart/{userId}/{itemId}")]
        public IActionResult AddItemToCart(int userId, int itemId)
        {
            var user = _context.Users.Include(u => u.ShoppingCart).FirstOrDefault(u => u.Id == userId);
            var item = _context.Items.FirstOrDefault(i => i.Id == itemId);

            if (user == null || item == null)
            {
                return NotFound("User or item not found.");
            }

            if (user.ShoppingCart == null)
            {
                user.ShoppingCart = new ShoppingCart { UserId = userId };
                _context.SaveChanges();
            }

            var cartItem = new CartItem { ItemId = itemId, CartId = user.ShoppingCart.Id };
            _context.UserCartItems.Add(cartItem);
            _context.SaveChanges();

            return Ok($"Item with ID {itemId} added to the cart for user ID {userId}.");
        }
    }
}