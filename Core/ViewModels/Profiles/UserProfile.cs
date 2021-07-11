using Core.Entities;
using Core.ViewModels.Responses.Users;

namespace Core.ViewModels.Profiles
{
    public class UserProfile : ProfileBase
    {
        protected override void DefaultMapping()
        {
            CreateMap<User, UserResponse>();
        }
    }
}
