using Boundless_Memories.Common.Database.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Memories
{
	public class Startup
    {
		public IConfigurationRoot Configuration { get; }
		public IHostingEnvironment HostingEnvironment { get; }

		public Startup(IHostingEnvironment env)
		{
			Configuration = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.Build();

			HostingEnvironment = env;
		}

		public void ConfigureServices(IServiceCollection services)
        {
			services.InitializeApplication();
			services.AddSingleton<IHostingEnvironment>(HostingEnvironment);

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			// TODO add authorization 
			//var connection = "Server=tcp:memories.database.windows.net,1433;Initial Catalog=Memories;Persist Security Info=False;User ID=alexander.lam;Password=m5m0r!5s;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
			var connection = @"Server=.\SQLEXPRESS;Database=Memories;Trusted_Connection=True;ConnectRetryCount=0";
			services.AddDbContext<MemoriesContext>(options => options.UseSqlServer(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment() || env.IsEnvironment("Localhost"))
            {
                app.UseDeveloperExceptionPage();
            }
			else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
			var strinsg = Configuration.GetSection("CORS:Origins").Get<string[]>();

			app.UseCors(builder => 
			builder.AllowAnyOrigin()
			//WithOrigins(Configuration.GetSection("CORS:Origins").Get<string[]>())
					.AllowAnyHeader()
					.AllowAnyMethod()
            );

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
