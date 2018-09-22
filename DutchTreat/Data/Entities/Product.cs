using System;
using System.Security.Policy;

namespace DutchTreat.Data.Entities
{
	public class Product
	{
		public Product()
		{
			ImageSource = "https://vignette.wikia.nocookie.net/project-pokemon/images/8/89/Pikachu.jpg/revision/latest?cb=20170122232506";
		}
		public int Id { get; set; }
		public string Name { get; set; }
		public decimal Attack { get; set; }
		public string ImageSource { get; set; }
	}
}
