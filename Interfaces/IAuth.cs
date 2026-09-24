using PracticeAuth.Models.DTOs;
using PracticeAuth.Models.Entity;

namespace PracticeAuth.Interfaces;

public interface IAuth
{
    public RegisterUserResponse Register(RegisterUserRequest userRequest);
    public RegisterUserRequest Login(LoginUserRequest  userRequest);
    
    //ToDo: Implement these functionalities later
    //public void Logout();
    //public void ChangePassword();
    //public void ChangeEmail();
    
}