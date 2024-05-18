using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetFamily.Application.Features.Accounts.Login;

namespace PetFamily.API.Controllers;

public class AccountController : ApplicationController
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

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(HttpContext.User.Claims.Select(x => x.Value));
    }
}