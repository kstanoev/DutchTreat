using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
	public class PokemonRepository : IPokemonRepository
	{
		private readonly PokemonDbContext _dbContext;

		public PokemonRepository(PokemonDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public void AddEntity(object model)
		{
			_dbContext.Add(model);
		}

		public IEnumerable<Order> GetAllOrders()
		{
			return _dbContext.Orders.Include(o => o.Items)
				.ThenInclude(i => i.Product)
				.ToList();
		}

		public IEnumerable<Product> GetAllProducts()
		{
			return _dbContext.Products.OrderBy(p => p.Name).ToList();
		}

		public Order GetOrderById(int id)
		{
			return _dbContext.Orders.Include(o => o.Items)
				.ThenInclude(i => i.Product)
				.Where(o => o.Id == id)
				.FirstOrDefault();
		}

		public IEnumerable<Product> GetProductByName(string desiredProductName)
		{
			return _dbContext.Products.Where(p => p.Name.Contains(desiredProductName)).ToList();
		}

		public bool SaveAll()
		{
			return _dbContext.SaveChanges() > 0;
		}
	}
}
