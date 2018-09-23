using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DutchTreat.Data
{
    public class Repository : IRepository
    {
        private readonly DbContext _dbContext;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddEntity(object model)
        {
            _dbContext.Add(model);
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            if (includeItems)
            {
                return _dbContext.Orders
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                    .ToList();
            }
            else
            {
                return _dbContext.Orders.ToList();
            }
        }

        public IEnumerable<Order> GetAllOrdersByUsername(string username, bool includeItems)
        {
            if (includeItems)
            {
                return _dbContext.Orders
                    .Where(o => o.User.UserName == username)
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                    .ToList();
            }
            else
            {
                return _dbContext.Orders
                    .Where(o => o.User.UserName == username)
                    .ToList();
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _dbContext.Products.OrderBy(p => p.Name).ToList();
        }

        public Order GetOrderById(string username, int id)
        {
            var result = _dbContext.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .Single(o => o.Id == id && o.User.UserName == username);

            return result;
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
