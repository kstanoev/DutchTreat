using System.Collections.Generic;
using DutchTreat.Data.Entities;

namespace DutchTreat.Data
{
	public interface IPokemonRepository
	{
		IEnumerable<Product> GetAllProducts();
		IEnumerable<Order> GetAllOrders();
		IEnumerable<Product> GetProductByName(string desiredProductName);
		Order GetOrderById(int id);
		void AddEntity(object model);
		bool SaveAll();
	}
}