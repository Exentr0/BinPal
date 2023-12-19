using Backend.Data;
using FluentValidation;

namespace Backend.Validation;


//Check if Item is unique in the database
public static class CustomValidators
{
    public static IRuleBuilderOptions<T, TProperty> IsUnique<T, TProperty>(
        this IRuleBuilder<T, TProperty> ruleBuilder, DataContext context, Func<T, TProperty> selector)
        where T : class
    {
        return ruleBuilder
            .Must(value => IsUniqueValue(context, selector, value))
            .WithMessage("{PropertyName} is already taken. Please choose a different one.");
    }
    private static bool IsUniqueValue<T, TProperty>(
        DataContext context, Func<T, TProperty> selector, TProperty value)
        where T : class
    {
        // Fetch the data from the database first and then apply the filter client-side
        return !context.Set<T>().AsEnumerable().Any(u => selector(u).Equals(value));
    }

    
    
    // Check if the number is greater than or equal to 0.01 and has at least 2 digits after the decimal point
    public static bool IsValidDecimal(decimal Number)
    {
        return Number >= 0 && decimal.Round(Number, 2) == Number;
    }
    
    //Checks that date is in the future
    public static bool IsFutureDate(DateTime date)
    {
        return date > DateTime.Now;
    }
    
    //Check if url is valid
    public static bool IsValidUrl(string url)
    {
        Uri uriResult;
        bool isValidUri = Uri.TryCreate(url, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        return isValidUri;
    }
}


