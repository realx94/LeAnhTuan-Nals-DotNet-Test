using Core.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Repositories
{
    public static class RepositoriesRegister
    {
		public static void UseRepositories(this IServiceCollection services)
		{
			services.AddScoped<IUserRepository, UserRepository>();

		}
	}
}
