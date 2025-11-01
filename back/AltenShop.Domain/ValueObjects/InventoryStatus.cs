namespace AltenShop.Domain.ValueObjects
{
	public sealed record InventoryStatus
	{
		/// <summary>
		/// Strongly typed inventory state with safe construction and JSON serialization.
		/// </summary>

		public static readonly InventoryStatus InStock = new("INSTOCK");
		public static readonly InventoryStatus LowStock = new("LOWSTOCK");
		public static readonly InventoryStatus OutOfStock = new("OUTOFSTOCK");

		public string Value { get; }
		private InventoryStatus(string value) => Value = value;

		public static InventoryStatus FromQuantity(int quantity) =>
		 quantity switch
		 {
			 > 10 => InStock,
			 > 0 => LowStock,
			 _ => OutOfStock,

		 };

		public static InventoryStatus FromString(string value) => value.ToUpperInvariant() switch
		{
			"INSTOCK" => InStock,
			"LOWSTOCK" => LowStock,
			"OUTOFSTOCK" => OutOfStock,
			_ => throw new ArgumentException($"Invalid inventory status: {value}")
		};


		public override string ToString() => Value;

	}
}
