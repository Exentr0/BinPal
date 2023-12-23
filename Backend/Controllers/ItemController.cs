using Azure.Core;
using Backend.Data;
using Backend.Models;
using Backend.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    private ItemAddingService _itemAddingService;


    public ItemController(ItemAddingService itemAddingService)
    {
        _itemAddingService = itemAddingService;
    }

    [HttpPost("upload-package")]
    public async Task<IActionResult> UploadPackage(IFormCollection formData, [FromServices] IValidator<Item> validator)
    {
        var packageInfo = JsonConvert.DeserializeObject<ItemAddingDto>( HttpContext.Request.Form["packageInfo"]);
        
        packageInfo.UploadedPictures = HttpContext.Request.Form.Files.Where(file => file.Name.Contains("UploadedPictures")).ToList();
        packageInfo.Releases = HttpContext.Request.Form.Files.Where(file => file.Name.Contains("Releases")).ToList();

        var newItem = new Item
        {
            PublisherId = 1, //Should be changed ASAP (якщо щось не робить то напевно бо в тебе в бд нема юзера з таким id)
            Name = packageInfo.GeneralInfo.Name,
            Price = packageInfo.GeneralInfo.Price ?? 0,
            Description = packageInfo.GeneralInfo.Description,
            License = "Default License", //Should be changed in the future
            Rating = 0,
            LikesAmount = 0,
            PublisherInfo = "Default Publisher Info" //Should be changed in the future
        };
        
        //Validate Item
        ValidationResult validationResult = await validator.ValidateAsync(newItem);
        
        // Check if validation failed
        if (!validationResult.IsValid)
        {
            // Convert validation errors to ModelState errors
            var modelStateDictionary = new ModelStateDictionary();
            foreach (FluentValidation.Results.ValidationFailure failure in validationResult.Errors)
            {
                modelStateDictionary.AddModelError(
                    failure.PropertyName,
                    failure.ErrorMessage);
            }

            // Return validation errors
            return ValidationProblem(modelStateDictionary);
        }
        else
        {
            await _itemAddingService.AddItemToDatabase(newItem, packageInfo);
            await _itemAddingService.AddItemPictures(newItem.Id, packageInfo.UploadedPictures);
            await _itemAddingService.AddItemReleases(newItem.Id, packageInfo.Releases);
        }
            
        return Ok();
    }
}
