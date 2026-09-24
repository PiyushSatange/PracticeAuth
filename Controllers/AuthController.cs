using Microsoft.AspNetCore.Mvc;
using PracticeAuth.Interfaces;
using PracticeAuth.Models.DTOs;
using PracticeAuth.Services;

namespace PracticeAuth.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuth _authService;

    public AuthController(IAuth auth)
    {
        _authService = auth;
    }
    
    [HttpPost("register")]
    public IActionResult Register(RegisterUserRequest request)
    {
        RegisterUserResponse response = _authService.Register(request);
        return Created("/api/auth/register", response);
    }
}