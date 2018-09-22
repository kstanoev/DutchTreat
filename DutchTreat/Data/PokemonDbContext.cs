using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DutchTreat.Data
{
	public class PokemonDbContext : IdentityDbContext<StoreUser>
	{
		public PokemonDbContext(DbContextOptions<PokemonDbContext> options)
			: base(options)
		{

		}
		public DbSet<Product> Products { get; set; }
		public DbSet<Order> Orders { get; set; }
	}
}
