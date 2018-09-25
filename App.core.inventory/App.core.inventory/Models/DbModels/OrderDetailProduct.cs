﻿using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace App.core.inventory.Models.DbModels
{
	[Table("OrderDetailProduct")]
	public class OrderDetailProduct
	{
		[PrimaryKey]
		[AutoIncrement]
		public int Id { get; set; }

		public string OrderCode { get; set; }

		public string UserCode { get; set; }

		public string DateCreated { get; set; }

		public int Quantity { get; set; }

		public string ProductCode { get; set; }

		public string ProviderCode { get; set; }

		public int Status { get; set; }
	}
}