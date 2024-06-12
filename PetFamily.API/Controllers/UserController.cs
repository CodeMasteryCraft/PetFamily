using Microsoft.AspNetCore.Mvc;
using PetFamily.Application.Features.Users.Login;

namespace PetFamily.API.Controllers;

public class UserController : ApplicationController
{
    [HttpPost]
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

    [HttpGet("test")]
    public async Task<IActionResult> Get()
    {
        return Ok(HttpContext.User.Claims.Select(x => x.Value));
    }
}