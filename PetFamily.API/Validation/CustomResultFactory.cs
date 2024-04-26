using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PetFamily.Domain.Common;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;

namespace PetFamily.API.Validation;

public class CustomResultFactory : IFluentValidationAutoValidationResultFactory
{
    public IActionResult CreateActionResult(
        ActionExecutingContext context,
        ValidationProblemDetails? validationProblemDetails)
    {
        if (validationProblemDetails is null)
        {
            return new BadRequestObjectResult("Invalid error");
        }

        List<Error> errors = validationProblemDetails?.Errors.Values
        .SelectMany(errorArray => errorArray.Select(Error.Deserialize))
        .ToList();

        var envelope = Envelope.Error(errors);

        return new BadRequestObjectResult(envelope);

        return new BadRequestObjectResult(envelope);
    }
}