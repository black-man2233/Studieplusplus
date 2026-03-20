using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using StudiePlusPlus.Application.Abstractions.Persistence;
using StudiePlusPlus.Application.Abstractions.Security;
using StudiePlusPlus.Application.Features.Auth;
using StudiePlusPlus.Domain.Users;

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
    private readonly ILogger<AuthController> _logger;

    public AuthController(
        IUserRepository      users,
        ILoginRepository     logins,
        IPasswordHasher      hasher,
        IConfiguration       configuration,
        IWebHostEnvironment  env,
        ILogger<AuthController> logger)
    {
        _users         = users;
        _logins        = logins;
        _hasher        = hasher;
        _configuration = configuration;
        _env           = env;
        _logger        = logger;
    }

    [HttpPost("loginmultiple")]
    public async Task<IActionResult> LoginMultiple([FromBody] List<LoginRequest> requests, CancellationToken ct)
    {
        var results = new List<object>();

        foreach (var item in requests)
        {
            var result = await AuthenticateAsync(item, ct);

            if (!result.Success)
            {
                results.Add(new
                {
                    identifier = item.Email,
                    success = false,
                    error = "Invalid identifier or password."
                });
            }
            else
            {
                results.Add(new
                {
                    identifier = item.Email,
                    success = true,
                    token = GenerateToken(result.UserId!.Value.ToString(), result.Email!)
                });
            }
        }

        return Ok(results);
    }

    private async Task<(bool Success, Guid? UserId, string Email)> AuthenticateAsync(LoginRequest request, CancellationToken ct)
    {
        var user = await FindUserByIdentifierAsync(request.Email, ct);
        if (user is null)
            return (false, null, null);

        var login = await _logins.GetByUserIdAsync(user.Id, ct);
        if (login is null)
            return (false, null, null);

        if (!_hasher.Verify(login.PasswordHash, request.Password))
            return (false, null, null);

        return (true, user.Id, user.Email.Value);
    }

    /// <summary>
    /// Sign in with email or full name and password – returns a JWT token.
    /// </summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken ct)
    {
        _logger.LogInformation("Login attempt for identifier={Identifier}", request.Email);

        var user = await FindUserByIdentifierAsync(request.Email, ct);
        if (user is null)
        {
            _logger.LogWarning("Login failed — no user found for identifier={Identifier}", request.Email);
            return Unauthorized("Invalid identifier or password.");
        }

        var login = await _logins.GetByUserIdAsync(user.Id, ct);
        if (login is null)
        {
            _logger.LogWarning("Login failed — no login record for userId={UserId}", user.Id);
            return Unauthorized("Invalid identifier or password.");
        }

        if (!_hasher.Verify(login.PasswordHash, request.Password))
        {
            _logger.LogWarning("Login failed — invalid password for userId={UserId}", user.Id);
            return Unauthorized("Invalid identifier or password.");
        }

        _logger.LogInformation("Login successful — userId={UserId} email={Email}", user.Id, user.Email.Value);
        return Ok(new { token = GenerateToken(user.Id.ToString(), user.Email.Value) });
    }

    /// <summary>
    /// Development only - returns all available logins with seeded credentials.
    /// Not available in Production.
    /// </summary>
    [HttpGet("available-logins")]
    public async Task<IActionResult> GetAvailableLogins(CancellationToken ct)
    {
        if (!_env.IsDevelopment())
            return NotFound();

        var users = await _users.GetAllAsync(ct);
        var logins = await _logins.GetAllAsync(ct);

        var availableLogins = users
            .Where(user => user.LoginId != Guid.Empty)
            .Join(
                logins,
                user => user.LoginId,
                login => login.Id,
                (user, login) => new
                {
                    userId = user.Id,
                    loginId = login.Id,
                    name = $"{user.FirstName} {user.LastName}",
                    username = user.Email.Value.Split('@', 2)[0],
                    email = user.Email.Value,
                    password = BuildSeedPassword(user.Email.Value)
                })
            .OrderBy(item => item.name)
            .ToList();

        _logger.LogInformation("Available logins requested - count={Count}", availableLogins.Count);
        return Ok(availableLogins);
    }

    private async Task<User> FindUserByIdentifierAsync(string identifier, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(identifier))
            return null;

        var trimmedIdentifier = identifier.Trim();
        var userByEmail = await _users.GetByEmailAsync(trimmedIdentifier, ct);
        if (userByEmail is not null)
            return userByEmail;

        var users = await _users.GetAllAsync(ct);
        var normalizedInput = NormalizeIdentifier(trimmedIdentifier);

        return users.FirstOrDefault(user =>
            NormalizeIdentifier($"{user.FirstName} {user.LastName}") == normalizedInput ||
            NormalizeIdentifier($"{user.FirstName}.{user.LastName}") == normalizedInput ||
            NormalizeIdentifier(user.Email.Value.Split('@', 2)[0]) == normalizedInput);
    }

    private static string NormalizeIdentifier(string value)
    {
        return new string(value
            .Trim()
            .ToLowerInvariant()
            .Where(char.IsLetterOrDigit)
            .ToArray());
    }

    private static string BuildSeedPassword(string email)
    {
        var localPart = email.Split('@', 2)[0];
        return $"{localPart}#2026!";
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "anonymous";
        _logger.LogInformation("Logout — userId={UserId}", userId);
        // JWT is stateless – logout is handled client-side by discarding the token.
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

        _logger.LogWarning("Dev token issued — this endpoint must never be reachable in Production");
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

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        _logger.LogDebug("JWT issued for userId={UserId} role={Role} expires={Expires}",
            userId, role, DateTime.UtcNow.AddHours(8));

        return tokenString;
    }
}
