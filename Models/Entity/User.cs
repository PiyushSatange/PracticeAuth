using System.ComponentModel.DataAnnotations;
using System.Data;

namespace PracticeAuth.Models.Entity;

public class User
{
    public int  Id { get; private set; }
    
    [Required]
    public string Name { get; private set; } = null!;
    
    [Required]
    [EmailAddress]
    public string Email { get; private set; } = null!;
    
    [Required]
    public string PasswordHash { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    
    private User(){}

    public User(string name, string email, string passwordHash)
    {
        Validate(name, email, passwordHash);
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        CreatedAt = DateTime.Now;
    }

    public void Update(string name, string email, string passwordHash)
    {
        Validate(Name, Email, PasswordHash);
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        UpdatedAt = DateTime.Now;
    }
    

    private void Validate(string name, string email, string passwordHash)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException($"Name can not be empty {nameof(name)}");
        if (string.IsNullOrEmpty(email))
            throw new ArgumentNullException($"Email can not be empty {nameof(email)}");
        if (string.IsNullOrEmpty(passwordHash))
            throw new ArgumentNullException($"PasswordHash can not be empty {nameof(passwordHash)}");
        //ToDo: Need to add validation for valid email
    }
}