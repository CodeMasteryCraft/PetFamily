using Microsoft.AspNetCore.Mvc;

namespace PetFamily.API.Controllers;

public class AccountController : ApplicationController
{
    [HttpPost]
    public async Task<IActionResult> Login(
        string email,
        string password)
    {
        
        
        return Ok("token");
    }
}