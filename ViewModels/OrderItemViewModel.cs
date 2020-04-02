using DutchTreat.Data.Entities;

namespace DutchTreat.ViewModels
{
	public class OrderItemViewModel
	{
		public int Id { get; set; }
		public Product Product { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
		public Order Order { get; set; }

		public int ProductId { get; set; }
		public string ProductCategory { get; set; }
		public string ProductSize { get; set; }
		public string ProductTitle { get; set; }
		public string ProductArtist { get; set; }
		public string ProductArtId { get; set; }
	}
}