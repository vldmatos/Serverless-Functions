using System;

namespace Catalog.Models
{
	public class Car
	{
		public string Id { get; set; } = Guid.NewGuid().ToString();

		public string Plate { get; set; }

		public string Model { get; set; }

		public decimal Price { get; set; }
	}
}
