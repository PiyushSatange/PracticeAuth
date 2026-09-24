using Microsoft.AspNetCore.Identity;
using PracticeAuth.Data;
using PracticeAuth.Interfaces;
using PracticeAuth.Models.DTOs;
using PracticeAuth.Models.Entity;

namespace PracticeAuth.Services;


public class AuthService : IAuth
{
    private readonly ApplicationDbContext _dbContext;

    public AuthService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    
    public RegisterUserResponse Register(RegisterUserRequest userRequest)
    {
        RegisterUserResponse response = new RegisterUserResponse();
        bool isEmailExist = _dbContext.Users.Any(u => u.Email == userRequest.Email);
        if (isEmailExist)
        {
            response.Message = "Email already exists";
            return response;
        }
        var hashedPassword = CreatePasswordHash(userRequest.Password);
        User user = new User(userRequest.Name, userRequest.Email, hashedPassword);
        _dbContext.Users.Add(user);
        int result = _dbContext.SaveChanges();
        if (result <= 0)
        {
            response.Message = "Something went wrong";
        }
        else
        {
            response.Message = "User registration Successful";
        }
        return response;
    }

    public RegisterUserRequest Login(LoginUserRequest userRequest)
    {
        throw new NotImplementedException();
    }

    private static string CreatePasswordHash(string actualPassword)
    {
        PasswordHasher<AuthService> hasher = new PasswordHasher<AuthService> ();
        string  hashedPassword = hasher.HashPassword(null!, actualPassword);
        return hashedPassword;
    }
}