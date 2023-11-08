using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Backend.Data;
using Backend.Validation;
using FluentValidation;

namespace Backend.Models;

public class Item
{
    [Key]
    public int Id { get; set; }
    
    public string Name { get; set; }

    public float Rating { get; set; }

    public int LikesAmount { get; set; } = 0;
    
    public int Price { get; set; }
    
    public string Description { get; set; }
    
    public string PublisherInfo { get; set; }
    
    public string License { get; set; }

    public string PicturesUrl { get; set; }

    public string ContentUrl { get; set; }

    [ForeignKey("PublisherId")] 
    public int PublisherId { get; set; }
    
    public User User { get; set; }
    
    public List<CartItem>? CartItems { get; set; }
    public List<Purchase>? Purchases { get; set; }
    public List<ItemReview>? Reviews { get; set; }
    public List<ItemCategory>? ItemCategories { get; set; }
}

public class ItemValidator : AbstractValidator<Item>
{
    private readonly DataContext _context;

    public ItemValidator(DataContext context)
    {
        _context = context;
        
        RuleFor(i => i.Id)
            .NotNull().WithMessage("Id can't be null");
        When(i => i.Id != null, () =>
            {
                RuleFor(i => i.Id)
                    .IsUnique(context, i => i.Id)
                    .WithMessage("Id Should be unique");
            });
        
        RuleFor(i => i.Name)
            .NotNull().WithMessage("Name can't be null");
        When(i => i.Name != null, () =>
        {
            RuleFor(i => i.Name)
                .NotEmpty()
                .WithMessage("Name can't be empty.")
                .MaximumLength(40)
                .WithMessage("Name must be a maximum of 40 characters.");
        });

        RuleFor(i => i.Rating)
            .NotNull().WithMessage("Rating can't be null");
        When(i => i.Rating != null, () =>
        {
            RuleFor(i => i.Rating)
                .InclusiveBetween(0, 5)
                .WithMessage("Rating must be a float value between 0 and 5.");
        });
       
        RuleFor(i => i.LikesAmount)
            .NotNull().WithMessage("LikesAmount can't be null");
        When(i => i.LikesAmount != null, () =>
        {
            RuleFor(i => i.LikesAmount)
                .GreaterThan(0)
                .WithMessage("LikesAmount must be greater than 0.");
        });
        
        RuleFor(i => i.Price)
            .NotNull().WithMessage("Price can't be null");
        When(i => i.Price != null, () =>
        {
            RuleFor(i => i.Price)
                .Must(price => CustomValidators.IsValidFloat(price));
        });

        RuleFor(i => i.Description)
            .NotNull().WithMessage("Description can't be null");
        When(i => i.Description != null, () =>
        {
            RuleFor(i => i.Description)
                .MaximumLength(500)
                .WithMessage("Description must be a maximum of 500 characters.");
        });
            
        
        RuleFor(i => i.PublisherInfo)
            .NotNull().WithMessage("PublisherInfo can't be null");
        When(i => i.PublisherInfo != null, () =>
        {
            RuleFor(i => i.PublisherInfo)
                .MaximumLength(500)
                .WithMessage("PublisherInfo must be a maximum of 500 characters.");
        });
        
        RuleFor(i => i.License)
            .NotNull().WithMessage("License can't be null");
        When(i => i.License != null, () =>
        {
            RuleFor(i => i.License)
                .MaximumLength(500)
                .WithMessage("License must be a maximum of 500 characters.");
        });

        RuleFor(i => i.PublisherId)
            .NotNull().WithMessage("PublisherId can't be null");
        
        RuleFor(i => i.PicturesUrl)
            .NotNull().WithMessage("Pictures Url cannot be null.");
        When(i => i.PicturesUrl != null, () =>
        {
            RuleFor(i => i.PicturesUrl)
                .Must(CustomValidators.IsValidUrl)
                .WithMessage("Pictures URL is not a valid URL.")
                .IsUnique(_context, i => i.PicturesUrl).WithMessage("Pictures URL should be unique");
        });
        
        RuleFor(i => i.ContentUrl)
            .NotNull().WithMessage("Content Url cannot be null.");
        When(i => i.ContentUrl != null, () =>
        {
            RuleFor(i => i.ContentUrl)
                .Must(CustomValidators.IsValidUrl)
                .WithMessage("Content URL is not a valid URL.")
                .IsUnique(_context, i => i.PicturesUrl).WithMessage("Content URL should be unique");
        });
    }
}