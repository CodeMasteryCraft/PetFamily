using Microsoft.AspNetCore.Mvc;
using PetFamily.Application.Features.Pets.GetPets;
using PetFamily.Infrastructure.Queries.Pets;
using PetFamily.Infrastructure.Queries.Volunteers.GetPets;

namespace PetFamily.API.Controllers;

public class PetController : ApplicationController
{
    // [HttpGet("ef-core")]
    // public async Task<IActionResult> Get(
    //     [FromServices] GetPetsQuery query,
    //     [FromQuery] GetPetsRequest request,
    //     CancellationToken ct)
    // {
    //     var response = await query.Handle(request, ct);
    //
    //     return Ok(response);
    // }

    [HttpGet("dapper")]
    public async Task<IActionResult> Get(
        [FromServices] GetAllPetsQuery query,
        [FromQuery] GetPetsRequest request,
        CancellationToken ct)
    {
        var response = await query.Handle();

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetById(
        [FromServices] GetByIdPetQuery handler,
        [FromQuery] GetByIdPetRequest request,
        CancellationToken ct)
    {
        var result = await handler.Handle(request, ct);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
    
    [HttpGet("pet")]
    public async Task<IActionResult> GetPetsByVolunteer(
        [FromServices] GetVolunteerPetsQuery handler,
        [FromQuery] GetVolunteerPetsRequest request,
        CancellationToken ct)
    {
        var result = await handler.Handle(request, ct);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}