using Core.Entities;
using Core.Extensions;
using Core.Repositories;
using Core.Services;
using Core.ViewModels.Requests.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services
{
    public class UserService : IUserService
    {
        IUserRepository repo;
        public UserService(IUserRepository repo)
        {
            this.repo = repo;
        }

        public User Authenticate(UserLoginRequest request)
        {
            if (string.IsNullOrEmpty(request?.UserName) || string.IsNullOrEmpty(request?.Password))
                return null;

            var user = repo.GetAll().FirstOrDefault(p => p.UserName.ToLower() == request.UserName.ToLower());

            // check if username exists
            if (user == null)
                return null;

            if (!user.PasswordHash.VerifyHashedPassword(request.Password))
                return null;

            return user;
        }

        public void Create(UserRegisterRequest request)
        {
            if (string.IsNullOrEmpty(request?.UserName) || string.IsNullOrEmpty(request?.Password))
                throw new ArgumentException("UserName or Password is missing");

            var checkExists = repo.GetAll().FirstOrDefault(p => p.UserName.ToLower() == request.UserName.ToLower());
            if (checkExists != null) throw new ArgumentException("UserName has already exist");

            var user = new User(request.UserName, request.Password.HashPassword(), request.Type);

            repo.Add(user);

            repo.SaveChanges();
        }

        public List<User> GetAll()
        {
            return repo.GetAll().ToList();
        }
    }
}
