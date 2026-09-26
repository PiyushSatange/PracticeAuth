using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using PracticeAuth.Data;
using PracticeAuth.Interfaces;
using PracticeAuth.Models;
using PracticeAuth.Models.DTOs;
using PracticeAuth.Models.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PracticeAuth.Services;


public class AuthService : IAuth
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IConfiguration _config;

    public AuthService(ApplicationDbContext dbContext, IConfiguration config)
    {
        _config = config;
        _dbContext = dbContext;
    }

    
    public async Task<ServiceResponse<RegisterUserResponse>> Register(RegisterUserRequest userRequest)
    {
        string email = userRequest.Email.Trim().ToLowerInvariant();
        bool isEmailExist = await _dbContext.Users.AnyAsync(u => u.Email == email);
        if (isEmailExist)
        {
            return ServiceResponse<RegisterUserResponse>
                .Fail(Constants.ErrorCodes.EmailAlreadyExists, "Email already exist");
        }
        var hashedPassword = CreatePasswordHash(userRequest.Password);
        User user = new User(userRequest.Name, email, hashedPassword);
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
        RegisterUserResponse response = new RegisterUserResponse
        {
            UserId = user.Id
        };
        return ServiceResponse<RegisterUserResponse>.Ok(response);
    }

    public async Task<ServiceResponse<LoginUserResponse>> Login(LoginUserRequest userRequest)
    {
        User user = await _dbContext.Users.FirstAsync(u => u.Email == userRequest.Email);
        if (user is null)
        {
            return ServiceResponse<LoginUserResponse>.Fail(Constants.ErrorCodes.UserNotFound,
                "Incorrect email or password");
        }
        bool isVerified = VerifyPassword(userRequest.Password, user.PasswordHash);
        if (!isVerified)
        {
            return ServiceResponse<LoginUserResponse>.Fail(Constants.ErrorCodes.UserNotFound,
                "Incorrect email or password");
        }
        string token = CreateAccessToken(user);
        LoginUserResponse response = new LoginUserResponse()
        {
            AccessToken = token
        };
        return ServiceResponse<LoginUserResponse>.Ok(response);
    }

    private string CreatePasswordHash(string actualPassword)
    {
        PasswordHasher<AuthService> hasher = new PasswordHasher<AuthService> ();
        string  hashedPassword = hasher.HashPassword(null!, actualPassword);
        return hashedPassword;
    }

    private bool VerifyPassword(string providedPassword, string actualPasswordHash)
    {
        PasswordHasher<AuthService> hasher = new PasswordHasher<AuthService>();
        PasswordVerificationResult isSame = hasher.VerifyHashedPassword(null!, actualPasswordHash, providedPassword);
        return isSame == PasswordVerificationResult.Success;
    }

    private string CreateAccessToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credientials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
        };

        var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], claims, signingCredentials: credientials, expires: DateTime.Now.AddMinutes(15) );
        return new JwtSecurityTokenHandler().WriteToken(token);    
    }
}