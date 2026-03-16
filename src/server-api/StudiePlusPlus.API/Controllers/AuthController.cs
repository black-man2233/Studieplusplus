using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using StudiePlusPlus.Application.Abstractions.Persistence;
using StudiePlusPlus.Application.Abstractions.Security;
using StudiePlusPlus.Application.Features.Auth;

namespace StudiePlusPlus.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository  _users;
    private readonly ILoginRepository _logins;
    private readonly IPasswordHasher  _hasher;
    private readonly IConfiguration   _configuration;
    private readonly IWebHostEnvironment _env;

    public AuthController(
        IUserRepository      users,
        ILoginRepository     logins,
        IPasswordHasher      hasher,
        IConfiguration       configuration,
        IWebHostEnvironment  env)
    {
        _users         = users;
        _logins        = logins;
        _hasher        = hasher;
        _configuration = configuration;
        _env           = env;
    }

    /// <summary>
    /// Sign in with email and password – returns a JWT token.
    /// </summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken ct)
    {
        var user = await _users.GetByEmailAsync(request.Email, ct);
        if (user is null)
            return Unauthorized("Invalid email or password.");

        var login = await _logins.GetByUserIdAsync(user.Id, ct);
        if (login is null)
            return Unauthorized("Invalid email or password.");

        if (!_hasher.Verify(login.PasswordHash, request.Password))
            return Unauthorized("Invalid email or password.");

        return Ok(new { token = GenerateToken(user.Id.ToString(), user.Email.Value) });
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        // JWT is stateless – logout is handled client-side by discarding the token.
        // TODO: implement token blacklisting if needed.
        return Ok("Logged out.");
    }

    /// <summary>
    /// Development only – returns a test JWT token without credentials.
    /// Not available in Production.
    /// </summary>
    [HttpGet("dev-token")]
    public IActionResult DevToken()
    {
        if (!_env.IsDevelopment())
            return NotFound();

        return Ok(new { token = GenerateToken("dev-user", "dev@studieplusplus.dk", role: "Admin") });
    }

    private string GenerateToken(string userId, string email, string role = "User")
    {
        var key      = _configuration["Jwt:Key"]!;
        var issuer   = _configuration["Jwt:Issuer"]!;
        var audience = _configuration["Jwt:Audience"]!;

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Email,          email),
            new Claim(ClaimTypes.Role,           role),
        };

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var token = new JwtSecurityToken(
            issuer:             issuer,
            audience:           audience,
            claims:             claims,
            expires:            DateTime.UtcNow.AddHours(8),
            signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
