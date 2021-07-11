using Core.ViewModels.Requests.Users;
using FluentValidation;

namespace Domain.Validators
{
    public class UserRegisterRequestValidator : CoreValidator<UserRegisterRequest>
    {
        public UserRegisterRequestValidator()
        {
            RuleFor(p => p.UserName).NotEmpty().MaximumLength(300);

            RuleFor(p => p.Password).NotEmpty().MaximumLength(300);

            RuleFor(p => p.Type).IsInEnum();
        }
    }

    public class UserLoginRequestValidator : CoreValidator<UserLoginRequest>
    {
        public UserLoginRequestValidator()
        {
            RuleFor(p => p.UserName).NotEmpty().MaximumLength(300);

            RuleFor(p => p.Password).NotEmpty().MaximumLength(300);
        }
    }
}
