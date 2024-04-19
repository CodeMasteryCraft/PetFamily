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

        var validationError = validationProblemDetails.Errors.First();

        var errorString = validationError.Value.First();

        var error = Error.Deserialize(errorString);

        var envelope = Envelope.Error(error);

        return new BadRequestObjectResult(envelope);
    }
}