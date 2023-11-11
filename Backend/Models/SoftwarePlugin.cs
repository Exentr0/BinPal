using System.ComponentModel.DataAnnotations.Schema;
using Backend.Data;
using FluentValidation;

namespace Backend.Models;

public class SoftwarePlugin
{
    [ForeignKey("SoftwareId")]
    public int SoftwareId { get; set; }

    [ForeignKey("PluginId")] 
    public int PluginId { get; set; }
    
    public Software Software { get; set; }
    
    public Plugin Plugin { get; set; }
}

public class SoftwarePluginValidator : AbstractValidator<SoftwarePlugin>
{
    public SoftwarePluginValidator()
    {
         RuleFor(sp => sp.PluginId)
            .NotNull()
            .WithMessage("PluginId can't be null");;

        RuleFor(sp => sp.SoftwareId)
            .NotNull()
            .WithMessage("SoftwareId can't be null");;
    }
}