using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.core.inventory.Models;
using App.core.inventory.Views.Count;
using Xamarin.Forms;


namespace App.core.inventory
{
	public partial class MainPage : MasterDetailPage
	{
		///public List<MasterPageItem> menuList { get; set; }
		public List<MasterPageItem> menuList { get; set; }

		public MainPage()
		{
			InitializeComponent();
			menuList = new List<MasterPageItem>();
		}

		public void LoadMenu()
		{
			menuList.Add(new MasterPageItem
			{
				Title = "Plan de Conteo",
				Icon = "conteo.png",
				TargetType = typeof(PlanList)
			});
			menuList.Add(new MasterPageItem
			{
				Title = "Recepcion",
				Icon = "conteo.png",
				TargetType = typeof(PlanList)
			});
			menuList.Add(new MasterPageItem
			{
				Title = "Tansferencia entre Bodegas",
				Icon = "conteo.png",
				TargetType = typeof(PlanList)
			});
			menuList.Add(new MasterPageItem
			{
				Title = "Salida a Produccion",
				Icon = "conteo.png",
				TargetType = typeof(PlanList)
			});
			menuList.Add(new MasterPageItem
			{
				Title = "Cambio de Producto",
				Icon = "conteo.png",
				TargetType = typeof(PlanList)
			});

			navigationDrawerList.ItemsSource = (IEnumerable)this.menuList;
			Detail = (Page)new NavigationPage((Page)Activator.CreateInstance(typeof(MainMenuDetail)))
			{
				BarBackgroundColor = Color.FromHex("#278C19"),
				BarTextColor = Color.White
			};
		}

		private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			MasterPageItem selectedItem = e.SelectedItem as MasterPageItem;
			if (selectedItem == null)
				return;
			Detail = (Page)new NavigationPage((Page)Activator.CreateInstance(selectedItem.TargetType))
			{
				BarBackgroundColor = Color.FromHex("#278C19"),
				BarTextColor = Color.White
			};
			navigationDrawerList.SelectedItem = (object)null;
			IsPresented = false;
		}


	}
}
