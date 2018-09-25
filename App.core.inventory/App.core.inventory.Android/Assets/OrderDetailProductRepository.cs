using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
	public class OrderDetailProductRepository
	{
		private readonly SQLiteAsyncConnection conn;

		public OrderDetailProductRepository(string dbPath)
		{
			this.conn = new SQLiteAsyncConnection(dbPath, true);
			this.conn.CreateTableAsync<OrderDetailProduct>().Wait();
		}

		public async Task<OrderDetailProduct> GetOrderDetailProduct(string code)
		{
			OrderDetailProduct orderDetailProduct = await conn.Table<OrderDetailProduct>().Where(a => a.OrderCode == code).FirstOrDefaultAsync();
			return orderDetailProduct;
		}

		public async Task<List<OrderDetailProduct>> GetOrderDetailProductByArray(string[] arrayCode)
		{
			List<OrderDetailProduct> listAsync = await conn.Table<OrderDetailProduct>().Where(a => arrayCode.Contains(a.OrderCode)).ToListAsync();
			return listAsync;
		}

		public async Task<List<OrderDetailProduct>> GetOrderDetailProductByCode(List<OrderDetailProduct> codes)
		{
			string[] myList = codes.Select(a => a.OrderCode).ToArray<string>();
			List<OrderDetailProduct> listAsync = await conn.Table<OrderDetailProduct>().Where(a => myList.Contains(a.OrderCode)).ToListAsync();
			return listAsync;
		}

		public async Task<OrderDetailProduct> Add(OrderDetailProduct add)
		{
			int success = 0;
			try
			{
				int num = await conn.InsertAsync(add);
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
