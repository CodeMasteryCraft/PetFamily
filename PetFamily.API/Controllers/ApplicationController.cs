using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Validation;
using PetFamily.Domain.Common;

namespace PetFamily.API.Controllers;

[ApiController]
public class ApplicationController : ControllerBase
{
    protected new IActionResult Ok(object? result = null)
    {
        var envelope = Envelope.Ok(result);
        return base.Ok(envelope);
    }

    protected IActionResult BadRequest(Error? error)
    {
        var envelope = Envelope.Error(error);
        return base.BadRequest(envelope);
    }

    protected IActionResult NotFound(Error? error)
    {
        var envelope = Envelope.Error(error);
        return base.NotFound(envelope);
    }

    protected IActionResult AsEnvelopeResult<TValue>(
        Result<TValue, IReadOnlyList<Error>> result,
        Func<Envelope, IActionResult>? error = null)
    {
        if (result.IsSuccess)
            return base.Ok(Envelope.Ok(result.Value));

        return (error ?? base.BadRequest)(Envelope.Error([.. result.Error]));
    }
}