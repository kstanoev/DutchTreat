namespace DutchTreat.Data.Entities
{
	public class OrderItem
    {
		public int Id { get; set; }
		public Product Product { get; set; }
		public Order Order { get; set; }
		public decimal Price { get; set; }

	}
}
