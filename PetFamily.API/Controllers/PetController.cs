using Contracts.Pets.Requests;
using Microsoft.AspNetCore.Mvc;
using PetFamily.Application.Services;

namespace PetFamily.API.Controllers;

[Route("[controller]")]
public class PetController : ApplicationController
{
    private readonly PetsService _petsService;

    public PetController(PetsService petsService)
    {
        _petsService = petsService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePetRequest request, CancellationToken ct)
    {
        var idResult = await _petsService.CreatePet(request, ct);

        if (idResult.IsFailure)
            return BadRequest(idResult.Error);

        return Ok(idResult.Value);
    }
    
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetPetsByPageRequest request, CancellationToken ct)
    {
        var response = await _petsService.Get(request, ct);

        return Ok(response);
    }
}