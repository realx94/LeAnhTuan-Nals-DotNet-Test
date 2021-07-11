using AutoMapper;

namespace Core.ViewModels.Profiles
{
    public abstract class ProfileBase : Profile
    {
        protected ProfileBase()
        {
            DefaultMapping();
        }

        protected virtual void DefaultMapping()
        {

        }

    }
}
