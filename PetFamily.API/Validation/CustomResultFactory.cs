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
        /// ���� WithError �� ����� ��������, �������������� �� �������
        static Error? FromString(string value)
        {
            try { return Error.Deserialize(value); } catch { return null; }
        };

        return validationProblemDetails is null /// �� ����, ������ ���� �� ������ �� ����� "������"
            ? throw new ArgumentNullException(nameof(validationProblemDetails))
            : new BadRequestObjectResult(
                Envelope.Error(validationProblemDetails.Errors
                    .Select(error => error.Value
                        .Select(value => FromString(value))
                        .Where(error => error != null)
                        )
                    .SelectMany(x => x)
                    .ToArray())
        );
    }
}