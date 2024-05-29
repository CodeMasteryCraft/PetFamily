using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using PetFamily.Application.Features.Volunteers.CreatePet;
using PetFamily.Application.Features.Volunteers.DeletePhoto;
using PetFamily.Application.Features.Volunteers.UploadPhoto;
using PetFamily.Application.Providers;
using PetFamily.Infrastructure.Queries.Volunteers;
using PetFamily.Infrastructure.Queries.Volunteers.GetVolunteerById;
using PetFamily.Infrastructure.Queries.Volunteers.GetVolunteers;

namespace PetFamily.API.Controllers;

public class VolunteerController : ApplicationController
{
    private readonly ICacheProvider _cache;

    public VolunteerController(ICacheProvider cache)
    {
        _cache = cache;
    }

    [HttpPost("pet")]
    // [HasPermission(Permissions.Pets.Create)]
    public async Task<IActionResult> Create(
        [FromServices] CreatePetHandler handler,
        [FromBody] CreatePetRequest request,
        CancellationToken ct)
    {
        var idResult = await handler.Handle(request, ct);

        if (idResult.IsFailure)
            return BadRequest(idResult.Error);

        return Ok(idResult.Value);
    }

    [HttpPost("photo")]
    public async Task<IActionResult> UploadPhoto(
        [FromServices] UploadVolunteerPhotoHandler handler,
        [FromForm] UploadVolunteerPhotoRequest request,
        CancellationToken ct)
    {
        var result = await handler.Handle(request, ct);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<ActionResult<GetVolunteersResponse>> GetVolunteers(
        [FromServices] GetVolunteersQuery query,
        CancellationToken ct)
    {
        var response = await query.Handle(ct);
        return Ok(response);
    }

    [HttpGet("photo")]
    public async Task<IActionResult> GetPhotos(
        [FromServices] GetVolunteerByIdQuery handler,
        [FromQuery] GetVolunteerPhotoRequest request,
        CancellationToken ct)
    {
        var result = await handler.Handle(request, ct);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpDelete("photo")]
    public async Task<IActionResult> DeletePhoto(
        [FromServices] DeleteVolunteerPhotoHandler handler,
        [FromQuery] DeleteVolunteerPhotoRequest request,
        CancellationToken ct)
    {
        var result = await handler.Handle(request, ct);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}