using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DutchTreat
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = CreateWebHostBuilder(args).Build();

			//SeedWithData(host);

			host.Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.ConfigureAppConfiguration(SetupConfiguration)
				.UseStartup<Startup>();

		private static void SetupConfiguration(WebHostBuilderContext ctx, IConfigurationBuilder builder)
		{
			// Removing default configuration options
			builder.Sources.Clear();

			// Lowest to highest priority
			builder.AddXmlFile("config.xml", optional: true)
				.AddJsonFile("config.json", optional: false, reloadOnChange: true)
				.AddEnvironmentVariables();
		}

		//private async static Task SeedWithData(IWebHost host)
		//{
		//	var scopeFactory = host.Services.GetService<IServiceScopeFactory>();
		//	using (var scope = scopeFactory.CreateScope())
		//	{
		//		var seeder = scope.ServiceProvider.GetService<SampleDataSeeder>();
		//		await seeder.SeedAsync();
		//	}
		//}
	}
}
