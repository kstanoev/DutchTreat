using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace DutchTreat
{
	public class Startup
	{
		private readonly IConfiguration config;

		public Startup(IConfiguration config)
		{
			this.config = config;
		}
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddIdentity<StoreUser, IdentityRole>(cfg =>
				{
					cfg.User.RequireUniqueEmail = true;
					cfg.Password.RequireLowercase = false;
					cfg.Password.RequireUppercase = false;
					cfg.Password.RequireNonAlphanumeric = false;
					cfg.Password.RequiredLength = 3;
				})
				.AddEntityFrameworkStores<PokemonDbContext>();

            services.AddAuthentication().
                AddCookie().
                AddJwtBearer();

			services.AddDbContext<PokemonDbContext>(cfg =>
			{
				cfg.UseSqlServer(this.config.GetConnectionString("PokemonConnectionString"));
			});

			services.AddAutoMapper();

			services.AddTransient<PokemonSampleDataSeeder>();

			services.AddScoped<IPokemonRepository, PokemonRepository>();

			services.AddTransient<IMessageService, MessageService>();


			services.AddMvc()
				.SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1)
				.AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

		}

		// This method gets called by the ru ntime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseExceptionHandler("/error");

				//app.UseDeveloperExceptionPage();

				// Seed the database
				using (var scope = app.ApplicationServices.CreateScope())
				{
					var seeder = scope.ServiceProvider.GetService<PokemonSampleDataSeeder>();
					seeder.SeedAsync().Wait();
				}
			}
			else
			{
				app.UseExceptionHandler("/error");
			}

			app.UseStaticFiles();
			app.UseNodeModules(env);

			app.UseAuthentication();

			app.UseMvc(cfg =>
			{
				cfg.MapRoute("default",
					"{controller}/{action}/{id?}",
					new { controller = "PokemonIndex", Action = "Index" });
			});
		}
	}
}
