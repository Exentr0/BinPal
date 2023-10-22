using Backend;
using Backend.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class UserTestController : ControllerBase
{
    private readonly DataContext _context;

    public UserTestController(DataContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<UserTest>>> GetUserTests()
    {
        return Ok(await _context.UserTest.ToListAsync());
    }
}