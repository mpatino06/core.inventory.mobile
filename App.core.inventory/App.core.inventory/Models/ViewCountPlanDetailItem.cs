using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App.core.inventory.Models
{
	public class ViewCountPlanDetailItem
	{
		public int Id { get; set; }

		public int CountPlanId { get; set; }

		public string UserCode { get; set; }

		public string DateCreated { get; set; }

		public int Quantity { get; set; }

		public string ProductCode { get; set; }

		public int Count { get; set; }

		public string Description { get; set; }
	}
}