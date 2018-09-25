using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.core.inventory.Droid.Assets;
using Xamarin.Forms;

namespace App.core.inventory
{
	public partial class App : Application
	{
		private static object syncroot = new object();
		
		public static CountPlanRepository CountPlanRepo { get; private set; }

		public static ProviderRepository ProviderRepo { get; private set; }

		public static OrderDetailProductRepository OrderDetailProductRepo { get; set; }

		public static OrderRepository OrderRepo { get; set; }

		public static string _displayText { get; set; }


		public App(string displayText)
		{
			_displayText = displayText;
			InitializeComponent();
			CountPlanRepo = new CountPlanRepository(displayText);
			ProviderRepo = new ProviderRepository(displayText);
			OrderRepo = new OrderRepository(displayText);

			MainPage = new MainPage();
		}


		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
