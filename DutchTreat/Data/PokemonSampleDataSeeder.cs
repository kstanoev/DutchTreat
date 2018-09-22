using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DutchTreat.Data
{
	public class PokemonSampleDataSeeder
	{
		private readonly PokemonDbContext _dbContext;
		private readonly IHostingEnvironment _host;
		private readonly UserManager<StoreUser> _userManager;

		public PokemonSampleDataSeeder(PokemonDbContext dbContext, IHostingEnvironment host, UserManager<StoreUser> userManager)
		{
			_dbContext = dbContext;
			_host = host;
			_userManager = userManager;
		}

		public async Task SeedAsync()
		{
			_dbContext.Database.EnsureDeleted();
			_dbContext.Database.EnsureCreated();

			StoreUser user = await _userManager.FindByEmailAsync("kiril.stanoev@gmail.com");

			if (user == null)
			{
				user = new StoreUser()
				{
					FirstName = "Kiril",
					LastName = "Stanoev",
					Email = "kiril.stanoev@gmail.com",
					UserName = "kiro"
				};

				var result = await _userManager.CreateAsync(user, "1234");

				if (result != IdentityResult.Success)
				{
					throw new InvalidOperationException("Failed to create user");
				}
			}


			_dbContext.Database.OpenConnection();
			try
			{
				_dbContext.Database.ExecuteSqlCommand("DELETE FROM dbo.Products");
				_dbContext.Database.ExecuteSqlCommand("DELETE FROM dbo.Orders");
				_dbContext.SaveChanges();
			}
			finally
			{
				_dbContext.Database.CloseConnection();
			}

			if (!_dbContext.Products.Any())
			{
				var filePath = Path.Combine(_host.ContentRootPath, "Data\\sampleProductsData.json");
				var json = File.ReadAllText(filePath);
				var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
				_dbContext.Products.AddRange(products);
				_dbContext.Database.OpenConnection();
				try
				{
					_dbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Products ON");
					_dbContext.SaveChanges();
					_dbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Products OFF");
				}
				finally
				{
					_dbContext.Database.CloseConnection();
				}

				var order = new Order() { Id = 1 };
				order.User = user;
				order.Items = new List<OrderItem>()
					{
						new OrderItem()
						{
							Product = products.First(),
							Price = 123,
							//Order = order
						}
					};
				_dbContext.Orders.Add(order);

				_dbContext.Database.OpenConnection();
				try
				{
					_dbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Orders ON");
					_dbContext.SaveChanges();
					_dbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Orders OFF");
				}
				finally
				{
					_dbContext.Database.CloseConnection();
				}

				_dbContext.SaveChanges();
			}
		}
	}
}
