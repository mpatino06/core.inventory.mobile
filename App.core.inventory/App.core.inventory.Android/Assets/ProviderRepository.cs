using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App.core.inventory.Models.DbModels;
using SQLite;

namespace App.core.inventory.Droid.Assets
{
	public class ProviderRepository
	{

		private readonly SQLiteAsyncConnection conn;

		public ProviderRepository(string dbPath)
		{
			this.conn = new SQLiteAsyncConnection(dbPath, true);
			this.conn.CreateTableAsync<Provider>().Wait();
		}

		public async Task<List<Provider>> GetProviderName(string name)
		{
			List<Provider> listAsync = new List<Provider>();
			try
			{
				Provider p1 = new Provider()
				{
					Code = "P1",
					Name = "Proveedor 1",
					Description = "Proveedor 1",
					Barcode = "P1"
				};
				var _p1 = this.conn.InsertAsync(p1);
				Provider p2 = new Provider()
				{
					Code = "P2",
					Name = "Proveedor 2",
					Description = "Proveedor 2",
					Barcode = "P2"
				};
				var _p2 = this.conn.InsertAsync(p2);
				Provider p3 = new Provider()
				{
					Code = "P3",
					Name = "Proveedor 3",
					Description = "Proveedor 3",
					Barcode = "P3"
				};
				var _p3 = this.conn.InsertAsync(p3);
				Provider p4 = new Provider()
				{
					Code = "P4",
					Name = "Proveedor 4",
					Description = "Proveedor 4",
					Barcode = "P4"
				};
				var _p4 = this.conn.InsertAsync(p4);
				Provider p5 = new Provider()
				{
					Code = "P5",
					Name = "Proveedor 5",
					Description = "Proveedor 5",
					Barcode = "P5"
				};
				var _p5 = this.conn.InsertAsync(p5);
			
				listAsync = await conn.Table<Provider>().Where(a=> a.Name ==  name).ToListAsync();

				return listAsync;
		}
      catch (Exception ex)
      {
        return null;
      }
}
  }
}
