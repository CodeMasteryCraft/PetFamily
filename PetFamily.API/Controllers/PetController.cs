using Microsoft.AspNetCore.Mvc;
using PetFamily.Application.Features.Pets.GetPets;
using PetFamily.Infrastructure.Queries.Pets;

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
    public async Task<IActionResult> Get(
        [FromServices] GetPetQuery handler,
        [FromQuery] GetPetRequest request,
        CancellationToken ct)
    {
        var result = await handler.Handle(request, ct);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}