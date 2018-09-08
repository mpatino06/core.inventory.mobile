using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.core.inventory.Views.Count
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PlanList : ContentPage
	{
		private CountServices countServices;
		public string _user { get; set; }

		public PlanList ()
		{
			InitializeComponent ();
		}
	}
}
