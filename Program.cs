using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DutchTreat.Data;

namespace DutchTreatIku
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();

			SeedDb(host);

			host.Run();
		}

		private static void SeedDb(IHost host)
		{
			var scopeFactory = host.Services.GetService<IServiceScopeFactory>();


			using (var scope = scopeFactory.CreateScope())
			{
				var seeder = scope.ServiceProvider.GetService<DutchSeeder>();
				seeder.SeedAsync().Wait();
			}
			
		}


		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureAppConfiguration(SetupConfiguration)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});

		private static void SetupConfiguration(HostBuilderContext ctx, IConfigurationBuilder builder)
		{
			//removes the default config options
			builder.Sources.Clear();

			builder.AddJsonFile("config.json", false, true)
					.AddEnvironmentVariables();

		}
	}
}
