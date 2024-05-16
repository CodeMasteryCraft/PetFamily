using Microsoft.AspNetCore.Mvc;
using PetFamily.Application.Features.VolunteerApplications.ApplyVolunteerApplication;

namespace PetFamily.API.Controllers;

public class VolunteerApplicationController : ApplicationController
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromServices] ApplyVolunteerApplicationHandler handler,
        [FromBody] ApplyVolunteerApplicationRequest request,
        CancellationToken ct)
    {
        var response = await handler.Handle(request, ct);
        if (response.IsFailure)
            return BadRequest(response.Error);

        return Ok(response.Value);
    }
}