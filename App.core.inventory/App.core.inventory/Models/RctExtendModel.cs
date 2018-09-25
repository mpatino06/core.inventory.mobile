using System;
using System.Collections.Generic;
using System.Text;

namespace App.core.inventory.Models
{
	public class RctExtendModel
	{
		public int Id { get; set; }

		public string Code { get; set; }

		public string ProviderCode { get; set; }

		public string Lot { get; set; }

		public string DateCreated { get; set; }

		public int UserId { get; set; }

		public List<Detail> Details { get; set; }
	}
}
