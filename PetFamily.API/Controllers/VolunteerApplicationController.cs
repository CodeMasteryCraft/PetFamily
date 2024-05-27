using Microsoft.AspNetCore.Mvc;
using PetFamily.Application.Features.VolunteerApplications.ApplyVolunteerApplication;
using PetFamily.Application.Features.VolunteerApplications.ApproveVolunteerApplication;

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

    [HttpPost("approve")]
    public async Task<IActionResult> Approve(
        [FromServices] ApproveVolunteerApplicationHandler handler,
        [FromBody] ApproveVolunteerApplicationRequest request,
        CancellationToken ct)
    {
        var result = await handler.Handle(request, ct);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok();
    }
}