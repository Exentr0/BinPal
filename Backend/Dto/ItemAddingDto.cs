using Backend.Models;

namespace Backend.Services;

public class ItemAddingDto
{
    public GeneralInfo GeneralInfo { get; set; }
    public List<SoftwareDto> SupportedSoftwareList { get; set; }
    public List<PluginDto> RequiredPluginsList { get; set; }
    public List<CategoryDto> CategoriesList { get; set; }
    public List<IFormFile> UploadedPictures { get; set; }
    public List<IFormFile> Releases { get; set; }
}

public class GeneralInfo
{
    public string Name { get; set; }
    public decimal? Price { get; set; }
    public string Description { get; set; }
}