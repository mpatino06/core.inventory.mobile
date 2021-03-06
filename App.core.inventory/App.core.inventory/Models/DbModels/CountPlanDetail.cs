﻿using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace App.core.inventory.Models.DbModels
{
	[Table("CountPlanDetail")]
	public class CountPlanDetail
	{
		[AutoIncrement]
		[PrimaryKey]
		public int Id { get; set; }

		public int CountPlanId { get; set; }

		public string ProductCode { get; set; }

		public int Quantity { get; set; }

		public int? TotalCounted { get; set; }

		public string DateCreated { get; set; }

		public int UserIdCreated { get; set; }

		public string Warehouse { get; set; }

		public string Value2 { get; set; }

		public string Value3 { get; set; }

		public string Value4 { get; set; }

		public string Value5 { get; set; }

		public string DateUpdated { get; set; }

		public int UserIdUpdated { get; set; }
	}
}
