using PracticeAuth.Models;
using PracticeAuth.Models.DTOs;
using PracticeAuth.Models.Entity;

namespace PracticeAuth.Interfaces;

public interface IAuth
{
    public Task<ServiceResponse<RegisterUserResponse>> Register(RegisterUserRequest userRequest);
    public Task<ServiceResponse<LoginUserResponse>> Login(LoginUserRequest  userRequest);
    
    //ToDo: Implement these functionalities later
    //public void Logout();
    //public void ChangePassword();
    //public void ChangeEmail();
    
}