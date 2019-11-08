using Boundless_Memories.Repositories.ImageRepository;
using Memories.Common.Security;
using Memories.Repositories.User;
using Memories.Services.Authentication;
using Memories.Services.ImageManagement;
using Memories.Services.UserManagement;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Security.Principal;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class ApplicationServiceCollectionExtensions
	{
		public static void InitializeApplication(this IServiceCollection services)
		{
			//Authentication Middleware
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddTransient<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);
			services.AddScoped<IAuthorizationContext, AuthorizationContext>();
			services.AddScoped<IAuthenticationManagement, AuthenticationManagement>();

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
