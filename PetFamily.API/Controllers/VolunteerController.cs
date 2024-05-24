using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Minio;
using Minio.DataModel.Args;
using PetFamily.API.Attributes;
using PetFamily.Application.Features.Volunteers.CreatePet;
using PetFamily.Application.Features.Volunteers.CreateVolunteer;
using PetFamily.Application.Features.Volunteers.DeletePhoto;
using PetFamily.Application.Features.Volunteers.UploadPhoto;
using PetFamily.Domain.Common;
using PetFamily.Infrastructure.Providers;
using PetFamily.Infrastructure.Queries.Volunteers.GetVolunteerById;

namespace PetFamily.API.Controllers;

public class VolunteerController : ApplicationController
{
    private readonly CacheProvider _cacheProvider;

    public VolunteerController(CacheProvider cacheProvider)
    {
        _cacheProvider = cacheProvider;
    }
    
    [HttpPost]
    [HasPermission(Permissions.Volunteers.Create)]
    public async Task<IActionResult> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken ct)
    {
        var idResult = await _cacheProvider
            .GetOrCreateAsync("key", _ => handler.Handle(request, ct), cancellationToken: ct);
        
        if (idResult.IsFailure)
            return BadRequest(idResult.Error);

        return Ok(idResult.Value);
    }

    [HttpPost("pet")]
    [HasPermission(Permissions.Pets.Create)]
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