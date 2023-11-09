using Backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

public class GetProductOnRequest: ControllerBase
{
    private readonly DataContext _context;

    public GetProductOnRequest(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<object>> GetItem(
        [FromQuery] string searchQuery,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 12)
    {
        try
        {
            var query = _context.Items.AsQueryable();
            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(p => p.Name.Contains(searchQuery));
            }

            int totalCount = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var result = new
            {
                TotalCount = totalCount,
                Items = items
            };
            return Ok(result);
        }
        catch (Exception ek)
        {
            return BadRequest($"Error");
        }
    }
}