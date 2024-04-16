using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetFamily.API.Contracts;
using PetFamily.API.Mapping;
using PetFamily.Application.Interfaces;
using PetFamily.Application.Models;
using PetFamily.Domain;
using PetFamily.Infrastructure;

namespace PetFamily.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PetController : ControllerBase
{
    private readonly IPetService _perService;
    public PetController(IPetService petService)
    {
        _perService = petService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreatePetRequest request, CancellationToken ct)
    {
        CreatePetModel model = new PetMap().MapWith(request);

        await _perService.CreateAsync(model,ct);

        return Ok();
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {   
        return Ok();
    }
}