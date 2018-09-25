using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace App.core.inventory.Models.DbModels
{
	[Table("OrderTShirt")]
	public class OrderTShirt
	{
		[PrimaryKey]
		[AutoIncrement]
		public int Id { get; set; }

		public string Code { get; set; }

		public string Description { get; set; }

		public string ProviderCode { get; set; }

		public string Value1 { get; set; }

		public string Value2 { get; set; }

		public string Value3 { get; set; }

		public string Value4 { get; set; }

		public string Value5 { get; set; }

		public bool IsSelected { get; set; }
	}
}
