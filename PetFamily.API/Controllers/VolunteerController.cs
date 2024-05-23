using Microsoft.AspNetCore.Mvc;
using PetFamily.Application.Features.Volunteers.CreatePet;
using PetFamily.Application.Features.Volunteers.CreateVolunteer;
using PetFamily.Application.Features.Volunteers.DeletePhoto;
using PetFamily.Application.Features.Volunteers.UploadPhoto;
using PetFamily.Infrastructure.Queries.Volunteers.GetAllVolunteers;
using PetFamily.Infrastructure.Queries.Volunteers.GetPhoto;
using PetFamily.Infrastructure.Queries.Volunteers.GetVolunteer;


namespace PetFamily.API.Controllers;

public class VolunteerController : ApplicationController
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken ct)
    {
        var idResult = await handler.Handle(request, ct);

        if (idResult.IsFailure)
            return BadRequest(idResult.Error);

        return Ok(idResult.Value);
    }

    [HttpPost("pet")]
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

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll([FromServices] GetVolunteersQuery query,
        CancellationToken ct, int size = 0, int page = 0)
    {
        var idResult = await query.Handle(size, page, ct);
        if (idResult.IsFailure)
            return BadRequest(idResult.Error);
        return Ok(idResult.Value);
    }

    [HttpGet("GetById")]
    public async Task<IActionResult> GetById([FromQuery]GetVolunteerRequest request, 
       [FromServices]GetVolunteerQuery query,
        CancellationToken ct)
    {
        var idResult = await query.Handle(request, ct);
        if (idResult.IsFailure)
            return BadRequest(idResult.Error);
        return Ok(idResult.Value);
    }
}