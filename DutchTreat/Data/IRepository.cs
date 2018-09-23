using System.Collections.Generic;
using DutchTreat.Data.Entities;

namespace DutchTreat.Data
{
	public interface IRepository
	{
		IEnumerable<Product> GetAllProducts();
		IEnumerable<Order> GetAllOrders(bool includeItems);
	    IEnumerable<Order> GetAllOrdersByUsername(string username, bool includeItems);
        IEnumerable<Product> GetProductByName(string desiredProductName);
		Order GetOrderById(string username, int id);
		void AddEntity(object model);
		bool SaveAll();
	}
}