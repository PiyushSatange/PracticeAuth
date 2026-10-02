using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.NativeInterop;
using PracticeAuth.Interfaces;
using PracticeAuth.Models;
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
    public async Task<ActionResult> Register([FromBody] RegisterUserRequest request)
    {
        ServiceResponse<RegisterUserResponse> result = await _authService.Register(request);
        if (!result.Success)
        {
            return Conflict(result.ErrorMessage);
        }
        return Created($"/api/users/{result?.Data?.UserId}", result?.Data);
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginUserResponse>> Login(LoginUserRequest request)
    {
        ServiceResponse<LoginUserResponse> result = await _authService.Login(request);
        if (!result.Success)
        {
            return NotFound(result.ErrorMessage);
        }
        return Ok(result?.Data);
        
    }

    [HttpGet("hello")]
    [Authorize]
    public ActionResult Hello()
    {
        return Ok("Bolo");
    }
}