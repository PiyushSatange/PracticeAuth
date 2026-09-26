namespace PracticeAuth.Models.DTOs;

public class RegisterUserRequest
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
};

public class RegisterUserResponse
{
    public int? UserId { get; set; }
    public string? Message { get; set; }
}

public class LoginUserRequest
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class LoginUserResponse
{
    public string AccessToken { get; set; } = null!;
}