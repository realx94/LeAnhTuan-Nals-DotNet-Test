using Core.Entities;
using Core.Repositories;
using Core.Services;
using Domain.Services;
using Moq;
using System.Collections.Generic;
using Xunit;
using Core.Extensions;
using Core.Entities.Enums;
using System.Linq;
using Core.ViewModels.Requests.Users;
using System;

namespace UnitTest
{
    public class UserServiceTest
    {
        private Mock<IUserRepository> mockUserRepo;
        private IUserService userService;
        public UserServiceTest()
        {
            mockUserRepo = new Mock<IUserRepository>();
            var modelUsersMock = new List<User>()
            {
                new User("test", "test".HashPassword(), UserTypes.User),
                new User("test2", "test2".HashPassword(), UserTypes.Admin),
                new User("test3", "test3".HashPassword(), UserTypes.Partner),
            };
            mockUserRepo.Setup(s => s.GetAll()).Returns(modelUsersMock.AsQueryable());
            mockUserRepo.Setup(s => s.SaveChanges()).Returns(1);

            userService = new UserService(mockUserRepo.Object);

        }

        #region Create User
        [Fact]
        public void CreateUser_ValidData_NoExceptionReturn()
        {
            var mockUser = new User("testcreate", "testcreate".HashPassword(), UserTypes.Partner);
            mockUserRepo.Setup(p => p.Add(mockUser));
            //test
            UserRegisterRequest request = new UserRegisterRequest() { UserName = "testcreate", Password = "testcreate", Type = UserTypes.Partner };
            var result = userService.Create(request);
            // ASSERT
            Assert.Equal(mockUser.UserName, result.UserName);
            Assert.Equal(mockUser.Type, result.Type);
        }

        [Fact]
        public void CreateUser_MissingUserName_ArgumentException()
        {
            UserRegisterRequest request = new UserRegisterRequest() { UserName = string.Empty, Password = "testcreate", Type = UserTypes.Partner };
            //test
            Action act = () => userService.Create(request);
            //assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            //message
            Assert.Equal("UserName or Password is missing", exception.Message);
        }

        [Fact]
        public void CreateUser_ExistUserName_ArgumentException()
        {
            UserRegisterRequest request = new UserRegisterRequest() { UserName = "test", Password = "testcreate", Type = UserTypes.Partner };
            //test
            Action act = () => userService.Create(request);
            //assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            //message
            Assert.Equal("UserName has already exist", exception.Message);
        }

        [Fact]
        public void CreateUser_ExistUserNameUpper_ArgumentException()
        {
            UserRegisterRequest request = new UserRegisterRequest() { UserName = "TEST", Password = "testcreate", Type = UserTypes.Partner };
            //test
            Action act = () => userService.Create(request);
            //assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            //message
            Assert.Equal("UserName has already exist", exception.Message);
        }
        #endregion

        #region Authenticate
        const string existUserName = "test";
        [Fact]
        public void Authenticate_ExistUser_User()
        {
            //test
            UserLoginRequest request = new UserLoginRequest() { UserName = existUserName, Password = "test" };
            var result = userService.Authenticate(request);
            // ASSERT
            Assert.NotNull(result);
            Assert.Equal(request.UserName, result.UserName);
            Assert.True(result.PasswordHash.VerifyHashedPassword(request.Password));
        }

        [Fact]
        public void Authenticate_ExistUserUserNameUpper_Null()
        {
            //test
            UserLoginRequest request = new UserLoginRequest() { UserName = existUserName.ToUpper(), Password = "test" };
            var result = userService.Authenticate(request);
            // ASSERT
            Assert.NotNull(result);
            Assert.Equal(request.UserName.ToLower(), result.UserName.ToLower());
            Assert.True(result.PasswordHash.VerifyHashedPassword(request.Password));
        }

        [Fact]
        public void Authenticate_MissingUserName_Null()
        {
            //test
            UserLoginRequest request = new UserLoginRequest() { UserName = string.Empty, Password = "test" };
            var result = userService.Authenticate(request);
            // ASSERT
            Assert.Null(result);
        }

        [Fact]
        public void Authenticate_NotExistUser_Null()
        {
            //test
            UserLoginRequest request = new UserLoginRequest() { UserName = "notexistuser", Password = "test" };
            var result = userService.Authenticate(request);
            // ASSERT
            Assert.Null(result);
        }

        [Fact]
        public void Authenticate_ExistUserWrongPassword_Null()
        {
            //test
            UserLoginRequest request = new UserLoginRequest() { UserName = existUserName, Password = "wrongpassword" };
            var result = userService.Authenticate(request);
            // ASSERT
            Assert.Null(result);
        }
        #endregion
    }
}
