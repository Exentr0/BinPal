using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Backend.Data;
using Backend.Models;
using Backend.Services;
using Backend.Services.Storage;
using Microsoft.AspNetCore.Authorization;
namespace Backend.Controllers
//adding item to cart of particular user
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUserService _userService;
        private readonly ItemPicturesBlobService _itemPicturesBlobService;
        public CartController(DataContext context, IUserService userService, ItemPicturesBlobService itemPicturesBlobService)
        {
            _userService = userService;
            _context = context;
            _itemPicturesBlobService = itemPicturesBlobService;
        }

        
        [HttpPost("add/{itemId}")]
        [Authorize]
        public IActionResult AddItemToCart(int itemId)
        {
            var userName = _userService.GetMyName();
            var user = _context.Users.Include(u => u.ShoppingCart).FirstOrDefault(u => u.Username == userName);
            var item = _context.Items.FirstOrDefault(i => i.Id == itemId);

            if (user == null || item == null)
            {
                return NotFound("User or item not found.");
            }

            if (user.ShoppingCart == null)
            {
                user.ShoppingCart = new ShoppingCart { UserId = user.Id };
                _context.SaveChanges();
            }

            var cartItem = new CartItem { ItemId = itemId, CartId = user.ShoppingCart.Id };
            _context.UserCartItems.Add(cartItem);
            _context.SaveChanges();

            return Ok($"Item with ID {itemId} added to your cart.");
        }
        
        [HttpDelete("remove/{itemId}")]
        [Authorize]
        public IActionResult RemoveItemFromCart(int itemId)
        {
            var userName = _userService.GetMyName();
            var user = _context.Users.Include(u => u.ShoppingCart).FirstOrDefault(u => u.Username == userName);
            var item = _context.Items.FirstOrDefault(i => i.Id == itemId);

            if (user == null || item == null)
            {
                return NotFound("User or item not found.");
            }

            if (user.ShoppingCart == null)
            {
                return BadRequest("Shopping cart is empty.");
            }

            var cartItem = _context.UserCartItems.FirstOrDefault(ci => ci.ItemId == itemId && ci.CartId == user.ShoppingCart.Id);

            if (cartItem == null)
            {
                return NotFound("Item not found in the shopping cart.");
            }

            _context.UserCartItems.Remove(cartItem);
            _context.SaveChanges();

            return Ok($"Item with ID {itemId} removed from your cart.");
        }
        [HttpGet("get-cart-items")]
        [Authorize]
        public IActionResult GetCartItems()
        {
            var userName = _userService.GetMyName();
            var user = _context.Users.Include(u => u.ShoppingCart).ThenInclude(sc => sc.CartItems).ThenInclude(ci => ci.Item)
                .FirstOrDefault(u => u.Username == userName);

            if (user == null || user.ShoppingCart == null)
            {
                return NotFound("User or shopping cart not found.");
            }

            var cartItems = user.ShoppingCart.CartItems.Select(ci => new
            {
                ItemId = ci.Item.Id,
                ItemName = ci.Item.Name,
                ItemPrice = ci.Item.Price,
                Owner = ci.Item.PublisherId,
                License = ci.Item.License,
                PictureUrl = _itemPicturesBlobService.GetItemPictureUrlsAsync(ci.Item.Id).Result.FirstOrDefault()
                // Add other item properties as needed
            });

            return Ok(cartItems);
        }
    }
}