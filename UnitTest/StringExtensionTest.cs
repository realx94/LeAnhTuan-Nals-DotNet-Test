using Xunit;
using Core.Extensions;

namespace UnitTest
{
    public class StringExtensionTest
    {
        const string hashPassword = "ABQKRVhV5MkFoDRzSAXjg7dORCPqqxTBDOlk5WCPXge8+WwaeNkNhiNZY17A4O7CSQ==";//=> "test"

        [Fact]
        public void VerifyHashedPassword_ValidPassword_True()
        {
            var password = "test";
            var result = hashPassword.VerifyHashedPassword(password);
            Assert.True(result);
        }

        [Fact]
        public void VerifyHashedPassword_ValidPasswordFirstCharUpper_False()
        {
            var password = "Test";
            var result = hashPassword.VerifyHashedPassword(password);
            Assert.False(result);
        }

        [Fact]
        public void VerifyHashedPassword_WrongPassword_False()
        {
            var password = "wrongpassword";
            var result = hashPassword.VerifyHashedPassword(password);
            Assert.False(result);
        }

        [Fact]
        public void HashPassword_Password_True()
        {
            var password = "thisispassword";
            var result = password.HashPassword();
            //Assert
            Assert.True(result.VerifyHashedPassword(password));
        }

    }
}
