using Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Services
{
    public static class ServicesRegister
    {
		public static void UseServices(this IServiceCollection services)
		{

			services.AddScoped<IUserService, UserService>();

			services.AddScoped<IAuthenticationManager, JWTAuthenticationManager>();
		}
	}
}
