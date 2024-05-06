using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Contracts;
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

    protected IActionResult BadRequest(List<Error>? error)
    {
        var envelope = Envelope.Error(error);

        return base.BadRequest(envelope);
    }

    protected IActionResult NotFound(List<Error>? error)
    {
        var envelope = Envelope.Error(error);

        return base.NotFound(envelope);
    }
}