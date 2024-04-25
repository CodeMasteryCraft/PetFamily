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

        var validationError = validationProblemDetails.Errors;

        List<Error> errors = new List<Error>();

        foreach (var ve in validationError)
        {
            var errorArray = ve.Value;

            foreach (var er in errorArray)
                errors.Add(Error.Deserialize(er));
        }
        var envelope = Envelope.Error(errors);


        return new BadRequestObjectResult(envelope);
    }
}