using Microsoft.AspNetCore.Mvc;

namespace StudiePlusPlus.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login()
    {
        // TODO: validate credentials and return a signed JWT token
        return StatusCode(501, "Login is not yet implemented.");
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        // TODO: invalidate token / session
        return StatusCode(501, "Logout is not yet implemented.");
    }
}
