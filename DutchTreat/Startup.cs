using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using DbContext = DutchTreat.Data.DbContext;

namespace DutchTreat
{
	public class Startup
	{
		private readonly IConfiguration _config;

		public Startup(IConfiguration config)
		{
			_config = config;
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
				.AddEntityFrameworkStores<DbContext>();

            services.AddAuthentication().
                AddCookie().
                AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = _config["Tokens:Issuer"],
                        ValidAudience = _config["Tokens:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]))
                    };
                });

			services.AddDbContext<DbContext>(cfg =>
			{
				cfg.UseSqlServer(this._config.GetConnectionString("DbConnectionString"));
			});

			services.AddAutoMapper();

			services.AddTransient<SampleDataSeeder>();

			services.AddScoped<IRepository, Repository>();

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
					var seeder = scope.ServiceProvider.GetService<SampleDataSeeder>();
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
					new { controller = "App", Action = "Index" });
			});
		}
	}
}
