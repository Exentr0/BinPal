using Backend;
using Backend.Data;
using Backend.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
    
    [HttpPost]
    public async Task<ActionResult<UserTest>> CreateUserTest(UserTest userTest,
        [FromServices] IValidator<UserTest> validator)
    {
            ValidationResult validationResult = await validator.ValidateAsync(userTest);

            if (!validationResult.IsValid)
            {
                var modelStateDictionary = new ModelStateDictionary();
                foreach (ValidationFailure failure in validationResult.Errors)
                {
                    modelStateDictionary.AddModelError(
                        failure.PropertyName,
                        failure.ErrorMessage);
                }
                return ValidationProblem(modelStateDictionary);
            }

            // Add the new UserTest to the database
            _context.UserTest.Add(userTest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserTests", new { id = userTest.Id }, userTest);
    }
}