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

namespace App.core.inventory.Droid.Assets
{
	class OrderDetailProductRepository
	{
		private readonly SQLiteAsyncConnection conn;

		public OrderDetailProductRepository(string dbPath)
		{
			this.conn = new SQLiteAsyncConnection(dbPath, true);
			this.conn.CreateTableAsync<OrderDetailProduct>(CreateFlags.None).Wait();
		}

		public async Task<OrderDetailProduct> GetOrderDetailProduct(string code)
		{
			OrderDetailProduct orderDetailProduct = await this.conn.Table<OrderDetailProduct>().Where((Expression<Func<OrderDetailProduct, bool>>)(a => a.OrderCode == code)).FirstOrDefaultAsync();
			return orderDetailProduct;
		}

		public async Task<List<OrderDetailProduct>> GetOrderDetailProductByArray(string[] arrayCode)
		{
			List<OrderDetailProduct> listAsync = await this.conn.Table<OrderDetailProduct>().Where((Expression<Func<OrderDetailProduct, bool>>)(a => arrayCode.Contains<string>(a.OrderCode))).ToListAsync();
			return listAsync;
		}

		public async Task<List<OrderDetailProduct>> GetOrderDetailProductByCode(List<OrderDetailProduct> codes)
		{
			string[] myList = codes.Select<OrderDetailProduct, string>((Func<OrderDetailProduct, string>)(a => a.OrderCode)).ToArray<string>();
			List<OrderDetailProduct> listAsync = await this.conn.Table<OrderDetailProduct>().Where((Expression<Func<OrderDetailProduct, bool>>)(a => myList.Contains<string>(a.OrderCode))).ToListAsync();
			return listAsync;
		}

		public async Task<OrderDetailProduct> Add(OrderDetailProduct add)
		{
			int success = 0;
			try
			{
				int num = await this.conn.InsertAsync((object)add);
				success = num;
			}
			catch (Exception ex)
			{
				add = (OrderDetailProduct)null;
				throw;
			}
			return add;
		}
	}
}
