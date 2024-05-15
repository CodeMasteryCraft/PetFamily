using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Minio;
using Minio.DataModel.Args;
using PetFamily.API.Contracts;
using PetFamily.Application.Features.Volunteers.CreatePet;
using PetFamily.Application.Features.Volunteers.CreateVolunteer;
using PetFamily.Application.Features.Volunteers.UploadPhoto;
using PetFamily.Domain.Common;

namespace PetFamily.API.Controllers;

[Route("[controller]")]
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
        var validator = new UploadPhotoValidator();
        var validate = await validator.ValidateAsync(request, ct);
        if(!validate.IsValid)
            return BadRequest(validate.Errors);
        var result = await handler.Handle(request, ct);
        if (result.IsFailure)
            return BadRequest(result.Error);
        return Ok(result.Value);
    }


    [HttpGet("photo")]
    public async Task<IActionResult> GetPhoto(
        string photo,
        [FromServices] IMinioClient client)
    {
        var presignedGetObjectArgs = new PresignedGetObjectArgs()
            .WithBucket("images")
            .WithObject(photo)
            .WithExpiry(60 * 60 * 24);

        var url = await client.PresignedGetObjectAsync(presignedGetObjectArgs);

        return Ok(url);
    }
}