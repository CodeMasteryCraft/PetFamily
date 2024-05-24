using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Contracts;

namespace PetFamily.API.Controllers;

[ApiController]
[Route("[controller]")]
public abstract class ApplicationController : ControllerBase
{
    protected new ActionResult Ok(object? result = null)
    {
        var envelope = Envelope.Ok(result);

        return base.Ok(envelope);
    }

    protected IActionResult BadRequest(params ErrorInfo[] errors)
    {
        var envelope = Envelope.Error(errors);

        return base.BadRequest(envelope);
    }

    protected IActionResult NotFound(params ErrorInfo[] errors)
    {
        var envelope = Envelope.Error(errors);

        return base.NotFound(envelope);
    }
}