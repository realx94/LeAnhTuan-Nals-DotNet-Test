using Core.Entities.Enums;

namespace Core.ViewModels.Requests.Users
{
    public class UserRegisterRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserTypes Type { get; set; }
    }
}
