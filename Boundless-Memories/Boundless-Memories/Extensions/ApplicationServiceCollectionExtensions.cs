using Boundless_Memories.Repositories.ImageRepository;
using Memories.Repositories.User;
using Memories.Services.Authentication;
using Memories.Services.ImageManagement;
using Memories.Services.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class ApplicationServiceCollectionExtensions
	{
		public static void InitializeApplication(this IServiceCollection services)
		{
			// Repositories
			services.AddScoped<IUsersRepository, UsersRepository>();
			services.AddScoped<IImageRepository, ImageRepository>();

			// Services
			services.AddScoped<IImageManagement, ImageManagement>();
			services.AddScoped<IUsersManagement, UsersManagement>();
			services.AddScoped<IAuthenticationManagement, AuthenticationManagement>();

		}
	}
}
