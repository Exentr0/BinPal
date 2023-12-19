using Backend.Data;
using Backend.Models;
using Backend.Services.Storage;

namespace Backend.Services;

public class ItemAddingService
{
    private readonly DataContext _dataContext;

    private readonly ItemPicturesBlobService _itemPicturesBlobService;

    private readonly ItemReleasesBlobService _itemReleasesBlobService;

    public ItemAddingService(DataContext dataContext, ItemPicturesBlobService itemPicturesBlobService, ItemReleasesBlobService itemReleasesBlobService)
    {
        _dataContext = dataContext;
        _itemPicturesBlobService = itemPicturesBlobService;
        _itemReleasesBlobService = itemReleasesBlobService;
    }

    public async Task<Item> AddItemToDatabase(Item item, ItemAddingDto packageInfo)
    {
        using (var transaction = await _dataContext.Database.BeginTransactionAsync())
        {
            try
            {
                // Step 1: AddItemToDatabase
                await AddItem(item);

                // Step 2: AddItemSoftware
                await AddItemSoftware(item.Id, packageInfo.SupportedSoftwareList);

                // Step 3: AddItemPlugins
                await AddItemPlugins(item.Id, packageInfo.RequiredPluginsList);
                
                // Step 4: AddItemCategories
                await AddItemCategories(item.Id, packageInfo.CategoriesList);
                
                // If all steps are successful, commit the transaction
                await transaction.CommitAsync();

                // Return the created item
                return item;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }

    private async Task AddItem(Item item)
    {
        await _dataContext.Items.AddAsync(item);
        await _dataContext.SaveChangesAsync();
    }

    private async Task AddItemSoftware(int itemId, List<SoftwareDto> supportedSoftware)
    {
        foreach (var software in supportedSoftware)
        {
            var itemSoftware = new ItemSoftware
            {
                ItemId = itemId,
                SoftwareId = software.Id,
            };

            var validator = new SoftwareItemValidator();
            var validationResult = await validator.ValidateAsync(itemSoftware);

            if (validationResult.IsValid)
            {
                _dataContext.ItemSoftware.Add(itemSoftware);
                await _dataContext.SaveChangesAsync();
            }
            else
            {
                var validationErrors = validationResult.Errors
                    .Select(error => error.ErrorMessage)
                    .ToList();
            }
        }
    }

    private async Task AddItemPlugins(int itemId, List<PluginDto> requiredPlugins)
    {
        foreach (var plugin in requiredPlugins)
        {
            var itemPlugin = new ItemPlugin
            {
                ItemId = itemId,
                PluginId = plugin.Id
            };
            
            var validator = new ItemPluginValidator();
            var validationResult = await validator.ValidateAsync(itemPlugin);
            
            if (validationResult.IsValid)
            {
                _dataContext.ItemPlugins.Add(itemPlugin);
                await _dataContext.SaveChangesAsync();
            }
            else
            {
                var validationErrors = validationResult.Errors
                    .Select(error => error.ErrorMessage)
                    .ToList();
            }
        }
    }
    
    

    private async Task AddItemCategories(int itemId, List<CategoryDto> categoriesList)
    {
        foreach (var category in categoriesList)
        {
            var itemCategory = new ItemCategory
            {
                ItemId = itemId,
                CategoryId = category.Id
            };
            
            var validator = new ItemCategoryValidator();
            var validationResult = await validator.ValidateAsync(itemCategory);
            
            if (validationResult.IsValid)
            {
                _dataContext.ItemCategories.Add(itemCategory);
                await _dataContext.SaveChangesAsync();
            }
            else
            {
                var validationErrors = validationResult.Errors
                    .Select(error => error.ErrorMessage)
                    .ToList();
            }
        }
    }

    public async Task AddItemPictures(int itemId, List<IFormFile> pictures)
    {
        foreach (var picture in pictures)
        {
            await _itemPicturesBlobService.UploadBlobAsync(itemId, picture);
        }
    }
    
    public async Task AddItemReleases(int itemId, List<IFormFile> releases)
    {
        foreach (var release in releases)
        {
            await _itemReleasesBlobService.UploadBlobAsync(itemId, release);
        }
    }
}