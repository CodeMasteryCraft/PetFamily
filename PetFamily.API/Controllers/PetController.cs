using Contracts.Requests;
using Microsoft.AspNetCore.Mvc;
using PetFamily.Application;

namespace PetFamily.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PetController : ControllerBase
{
    private readonly PetsService _petsService;

    public PetController(PetsService petsService)
    {
        _petsService = petsService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreatePetRequest petRequest,
        [FromBody] CreateVaccinationRequest vaccinationRequest,
        [FromBody]CreatePhotoRequest photoRequest,
        [FromBody] CancellationToken ct)
    {
        var idResult = await _petsService.CreatePet(
            petRequest,
            photoRequest,
            vaccinationRequest, 
            ct);

        if (idResult.IsFailure)
            return BadRequest(idResult.Error);
        
        return Ok(idResult.Value);
    }
}