using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace DotNetGraphQL.Web.Entities.Validators;

public class BookValidator
{
    public static IEnumerable<ValidationResult> ValidateTitle(string value, string fieldName)
    {
        if (!Regex.IsMatch(value, @"^[a-zA-Z0-9 .,`'()ÁÉÍÓÚáéíóúñÑ\/\-']*$"))
        {
            yield return new ValidationResult(
                $"The {fieldName} field is invalid.", new[] {fieldName});
        }
    }
}