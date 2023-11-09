using Backend.Data;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/likes")]
[ApiController]
public class LikesAmountController: ControllerBase
{
    public readonly DataContext _context;

    public LikesAmountController(DataContext context)
    {
        _context = context;
    }

    [HttpGet("{itemId}")]
    public async Task<ActionResult<int>> GetLikesCount(int itemId)
    {
        try
        {
            var item = await _context.Items.FindAsync(itemId);
            if (item == null)
            {
                return NotFound();
            }

            int likesCount = item.LikesAmount;
            return Ok(likesCount);
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }
}