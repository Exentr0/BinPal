using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Backend.Data;
using Backend.Validation;
using FluentValidation;

namespace Backend.Models
{
    public class SoftwareCategory
    {
        [ForeignKey("SoftwareId")]
        public int SoftwareId { get; set; }

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public Software Software { get; set; }
    }

    public class SoftwareCategoryValidator : AbstractValidator<SoftwareCategory>
    {
        public SoftwareCategoryValidator()
        {
            RuleFor(sc => sc.SoftwareId)
                .NotNull()
                .WithMessage("SoftwareId can't be null");
            
            RuleFor(sc => sc.CategoryId)
                .NotNull()
                .WithMessage("CategoryId can't be null");
        }
    }
}