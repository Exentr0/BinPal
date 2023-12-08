using Backend.Data;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

public class SortingSearchResultController : Controller
{
    private readonly DataContext _context;
    
    public SortingSearchResultController(DataContext context)
    {
        _context = context;
    }
    
    
}