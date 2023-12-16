using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Backend.Data;
using Backend.Validation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

public class Plugin
{
    [Key] public int Id { get; set; }
    public string Name { get; set; }

    [ForeignKey("SoftwareId")]
    public int SoftwareId { get; set; }
    
    public Software Software { get; set; }
    
    public List<ItemPlugin>? ItemPlugins { get; set; }
}

public class PluginValidator : AbstractValidator<Plugin>
{
    private readonly DataContext _context;
    public PluginValidator(DataContext context)
    {
        _context = context;
        RuleFor(p => p.Id)
            .NotNull()
            .WithMessage("Id can't be null");
        When(p => p.Id != null, () =>
        {
            RuleFor(p => p.Id)
                .IsUnique(_context, p => p.Id)
                .WithMessage("Id must be unique");
        });

        RuleFor(p => p.SoftwareId)
            .NotNull()
            .WithMessage("SoftwareId can't be null");

        RuleFor(p => p.Name)
            .NotNull()
            .WithMessage("Name can't be null");
        When(p => p.Name != null, () =>
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("Name can't be empty")
                .IsUnique(_context, p => p.Name)
                .WithMessage("Name must be unique");
        });
    }
}