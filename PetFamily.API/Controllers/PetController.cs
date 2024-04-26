using Microsoft.AspNetCore.Mvc;
using PetFamily.Application.Pets.CreatePet;
using PetFamily.Application.Pets.GetPets;
using PetFamily.Infrastructure.Queries;
using PetFamily.Infrastructure.Queries.Pets;

namespace PetFamily.API.Controllers;

[Route("[controller]")]
public class PetController : ApplicationController
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromServices] CreatePetService sercvice,
        [FromBody] CreatePetRequest request,
        CancellationToken ct)
    {
        var idResult = await sercvice.Handle(request, ct);

        if (idResult.IsFailure)
            return BadRequest(idResult.Error);

        return Ok(idResult.Value);
    }

    [HttpGet("ef-core")]
    public async Task<IActionResult> Get(
        [FromServices] GetPetsQuery query,
        [FromQuery] GetPetsRequest request,
        CancellationToken ct)
    {
        var response = await query.Handle(request, ct);

        return Ok(response);
    }
}