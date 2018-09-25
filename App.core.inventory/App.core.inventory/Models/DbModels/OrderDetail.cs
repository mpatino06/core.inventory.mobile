using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace App.core.inventory.Models.DbModels
{
	[Table("OrderDetail")]
	public class OrderDetail
	{
		[AutoIncrement]
		[PrimaryKey]
		public int Id { get; set; }

		public string OrderCode { get; set; }

		public string ProductCode { get; set; }

		public int Quantity { get; set; }

		public string Value1 { get; set; }

		public string Value2 { get; set; }

		public string Value3 { get; set; }

		public string Value4 { get; set; }

		public string Value5 { get; set; }

		public string DateCreated { get; set; }

		public int? InitQuantity { get; set; }
	}
}
