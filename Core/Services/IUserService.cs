using Core.Entities;
using Core.ViewModels.Requests.Users;
using System.Collections.Generic;

namespace Core.Services
{
    public interface IUserService
    {
        User Authenticate(UserLoginRequest request);
        User Create(UserRegisterRequest request);
        List<User> GetAll();
    }
}
