using Microsoft.AspNetCore.Mvc;
using PetFamily.Application.Features.Users.Login;
using PetFamily.Application.Features.Users.Register;

namespace PetFamily.API.Controllers;

public class UserController : ApplicationController
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request,
        [FromServices] LoginHandler handler,
        CancellationToken ct)
    {
        var token = await handler.Handle(request, ct);
        if (token.IsFailure)
            return BadRequest(token.Error);

        return Ok(token.Value);
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest request,
        [FromServices] RegisterHandler handler,
        CancellationToken ct)
    {
        var result = await handler.Handle(request, ct);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok();
    }
}