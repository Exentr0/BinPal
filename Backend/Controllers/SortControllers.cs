using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
public class SortControllers : Controller
{
    private readonly DataContext _context;
    private List<Item> itemTable = new List<Item>();

    public SortControllers(DataContext context)
    {
        _context = context;
    }

    /*Тут треба підєднати фронт*/
    public IActionResult Index()
    {
        var categories = _context.Categories.ToList();

        /*CategoriesViewModel - це треба підключити к html, там де буде ті flash box*/
        var viewModel = new CategoriesViewModel
        {
            Categories = category
        };

        return View(viewModel);
    }
    [HttpGet]
    [Route("Slide Filtering")]
    public IActionResult Search(decimal min, decimal max)
    {
        var products = _context.Items.Where(p => p.Price >= min && p.Price <= max);
        return new JsonResult(products);
    }
    
    [HttpPost]
    [Route("All Catigories")]
    public IActionResult GetFilterCatigories(int[] id /*CATIGORIES*/)
    {
        var catigories = _context.Categories.Where(p => id.Contains(p.Id)).ToList();

        return PartialView( /*Тут назва той що буде на форонті*/"", catigories);
    }
    
    [HttpGet]
    [Route("Filter Platforms")]
    public IActionResult GetFilterPlatforms(int[] id)
    {
        var platforms = _context.SoftwareCategories.Where(p => id.Contains(p.SoftwareId));
        return PartialView("",platforms);
    }

    [HttpGet]
    [Route("Filter Raiting")]
    public IActionResult GetFilterRatings(float value)
    {
        var rating = _context.Items.Where(p => p.Rating == value);
        return new JsonResult(rating);
    }
    
    [HttpGet]
    [Route("Item in page")]
    public IEnumerable<Item> Get(int page = 1, int pageSize = 10)
    {
        var TotalCount = itemTable.Count;
        var totalPages =(int)Math.Ceiling((decimal)TotalCount / pageSize);
        var itemPerPage = itemTable.Skip((page -1) * pageSize).Take(pageSize).ToList();
        
        return itemPerPage;
    }
}