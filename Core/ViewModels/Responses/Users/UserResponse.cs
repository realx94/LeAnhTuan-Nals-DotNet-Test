using Core.Entities.Enums;

namespace Core.ViewModels.Responses.Users
{
    public class UserResponse
    {
        public string UserName { get; set; }
        public UserTypes Type { get; set; }
        public string TypeName => Type.ToString();
    }
}
