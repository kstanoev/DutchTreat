using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DutchTreat.Data
{
	public class DbContext : IdentityDbContext<StoreUser>
	{
		public DbContext(DbContextOptions<DbContext> options)
			: base(options)
		{

		}
		public DbSet<Product> Products { get; set; }
		public DbSet<Order> Orders { get; set; }
	}
}
