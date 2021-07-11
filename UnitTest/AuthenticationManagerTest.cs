using Core.Entities;
using Core.Entities.Enums;
using Core.Services;
using Core.Settings;
using Domain.Services;
using Moq;
using System.Security.Claims;
using Xunit;

namespace UnitTest
{
    public class AuthenticationManagerTest
    {
        private Mock<AppSettings> mockAppSettings;
        private IAuthenticationManager auth;
        public AuthenticationManagerTest()
        {
            mockAppSettings = new Mock<AppSettings>();

            mockAppSettings.Object.Auth=new AppSettings.AuthSetting
            {
                SecretKey = "rlVgPAE6VST1acnrv6KG6Bo9tSZ0UYFM"
            };

            auth = new JWTAuthenticationManager(mockAppSettings.Object);

        }

        [Fact]
        public void GenerateToken_ValidData_Token()
        {
            var user = new User("test","test", UserTypes.Admin);
            var token = auth.GenerateToken(user);
            Assert.NotEmpty(token);
        }

        [Fact]
        public void ValidateToken_ValidToken_Claims()
        {
            var user = new User("test", "test", UserTypes.Admin);
            var token = auth.GenerateToken(user);
            Assert.NotEmpty(token);

            var result = auth.ValidateToken(token);
            Assert.NotNull(result);

            Assert.Equal(user.UserName, result.FindFirst(ClaimTypes.Name)?.Value);
            Assert.Equal(user.Type.ToString(), result.FindFirst(ClaimTypes.Role)?.Value);
        }

        [Fact]
        public void ValidateToken_ValidTokenExpriedtime_Null()
        {
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InRlc3QiLCJuYW1laWQiOiIwIiwicm9sZSI6IkFkbWluIiwibmJmIjoxNjI1OTkzNzcwLCJleHAiOjE2MjU5OTM4MzAsImlhdCI6MTYyNTk5Mzc3MH0.Lja9e1EKdjSMUfoTh4symMoWCWnc7x7eXlROz5FJ3kc";
            var result = auth.ValidateToken(token);
            Assert.Null(result);
        }

        [Fact]
        public void ValidateToken_InValidToken_Null()
        {
            var token = "invalidtoken";
            Assert.NotEmpty(token);

            var result = auth.ValidateToken(token);
            Assert.Null(result);
        }
    }
}
