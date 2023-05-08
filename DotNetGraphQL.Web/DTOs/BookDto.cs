using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using DotNetGraphQL.Web.Entities.Validators;

namespace DotNetGraphQL.Web.DTOs;

public record BookBaseDto
{
    public string Title { get; init; }
    public DateTime PublishedOn { get; init; }
};

public record BookDto : BookBaseDto
{
    public Guid Id { get; init; }
};

public record BookForCreatingDto : IValidatableObject
{
    [Required] public string? Title { get; init; }
    [Required] public DateTime? PublishedOn { get; init; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var validationResult = new List<ValidationResult>();
        validationResult.AddRange(BookValidator.ValidateTitle(Title!, nameof(Title)));

        return validationResult;
    }
};

public record BookForUpdatingDto : BookForCreatingDto
{
    [Required] public Guid? Id { get; init; }
}