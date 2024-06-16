using Microsoft.AspNetCore.Mvc;
using PetFamily.Application.Features.Volunteers.CreatePet;
using PetFamily.Application.Features.Volunteers.DeletePhoto;
using PetFamily.Application.Features.Volunteers.UploadPhoto;
using PetFamily.Infrastructure.Queries.Volunteers.GetVolunteers;
using PetFamily.Infrastructure.Queries.Volunteers.GetPhoto;
using PetFamily.Infrastructure.Queries.Volunteers.GetVolunteer;

namespace PetFamily.API.Controllers;

public class VolunteerController : ApplicationController
{
    [HttpPost("pet")]
    // [HasPermission(Permissions.Pets.Create)]
    public async Task<IActionResult> CreatePet(
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
        [FromServices] GetVolunteerPhotoQuery handler,
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

    [HttpGet]
    public async Task<IActionResult> GetVolunteers([FromServices] GetVolunteersQuery query,
        [FromQuery] GetVolunteersRequest request,
        CancellationToken ct)
    {
        var idResult = await query.Handle(request, ct);
        if (idResult.IsFailure)
            return BadRequest(idResult.Error);
        return Ok(idResult.Value);
    }

    [HttpGet("GetById")]
    public async Task<IActionResult> GetById([FromQuery] GetVolunteerRequest request,
        [FromServices] GetVolunteerQuery query,
        CancellationToken ct)
    {
        var idResult = await query.Handle(request, ct);
        if (idResult.IsFailure)
            return BadRequest(idResult.Error);
        return Ok(idResult.Value);
    }
}