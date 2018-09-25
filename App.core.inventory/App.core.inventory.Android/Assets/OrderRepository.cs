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
using App.core.inventory.Models;
using App.core.inventory.Models.DbModels;
using SQLite;

namespace App.core.inventory.Droid.Assets
{
	public class OrderRepository
	{
		private readonly SQLiteAsyncConnection conn;

		public OrderRepository(string dbPath)
		{
			this.conn = new SQLiteAsyncConnection(dbPath, true);
			this.conn.CreateTableAsync<OrderTShirt>().Wait();
			this.conn.CreateTableAsync<OrderDetail>().Wait();
		}

		public async Task<List<OrderTShirt>> GetOrdersByProviderCode(string code)
		{
			List<OrderTShirt> listAsync = await conn.Table<OrderTShirt>().Where(a => a.ProviderCode == code).ToListAsync();
			return listAsync;
		}

		public async Task<List<OrderTShirt>> GetOrdersByCode(string code)
		{
			List<OrderTShirt> listAsync = await conn.Table<OrderTShirt>().Where(a => a.Code == code).ToListAsync();
			return listAsync;
		}

		public async Task<int> UpdateOrder(OrderTShirt order)
		{
			int num = await conn.UpdateAsync(order);
			return num;
		}

		public async Task<bool> OrderQueryExecute(string qry)
		{
			bool result = false;
			try
			{
				int num = await this.conn.ExecuteAsync(qry, Array.Empty<object>());
				result = true;
			}
			catch (Exception ex)
			{
				result = false;
				throw;
			}
			return result;
		}

		public async Task<List<OrderDetail>> GetOrderDetailByOrderAndProduct(string order, string product)
		{
			List<OrderDetail> listAsync = await conn.Table<OrderDetail>().Where(a => a.OrderCode == order && a.ProductCode == product).ToListAsync();
			return listAsync;
		}

		public async Task<int> UpdateOrderDetail(OrderDetail detail)
		{
			int success = 0;
			try
			{
				int num = await conn.UpdateAsync(detail);
				success = num;
			}
			catch (Exception ex)
			{
				throw;
			}
			return success;
		}

		public async Task<OrderDetail> GetOrderDetailByCode(string order)
		{
			OrderDetail orderDetail = await conn.Table<OrderDetail>().Where(a => a.OrderCode == order).FirstOrDefaultAsync();
			return orderDetail;
		}

		public async Task<List<ViewOrder>> GetListOrder(List<OrderTShirt> items)
		{
			try
			{
				string[] myList = items.Select<OrderTShirt, string>(a => a.Code).ToArray();
				string qry = "SELECT Od.Id,";
				qry += "Ord.Id AS IdOrder,";
				qry += "Ord.Code,";
				qry += "Ord.Description,";
				qry += "Ord.ProviderCode,";
				qry += "Ord.Value1,";
				qry += "Ord.Value2,";
				qry += "Pro.Name AS ProviderName,";
				qry += "Pro.Barcode AS ProviderBarcode,";
				qry += "Od.ProductCode,";
				qry += "Prod.Id AS IdProduct,";
				qry += "Prod.Description AS ProductName,";
				qry += "Prod.BarCode AS BarcodeProduct,";
				qry += "Od.Value1 AS OrderValue1,";
				qry += "Od.Value2 AS OrderValue2,";
				qry += "Od.Value3 AS OrderValue3,";
				qry += "Od.Value4 AS OrderValue4,";
				qry += "Od.Value5 AS OrderValue5,";
				qry += "Od.Quantity,";
				qry += "CASE WHEN odp.Status = 0 THEN SUM(Odp.Quantity)ELSE 0 END AS TotalProduct ";
				qry += "FROM OrderTShirt AS Ord INNER JOIN ";
				qry += "OrderDetail AS Od ON RTRIM(Ord.Code) = RTRIM(Od.OrderCode) INNER JOIN ";
				qry += "Provider AS Pro ON RTRIM(Ord.ProviderCode) = RTRIM(Pro.Code) LEFT OUTER JOIN ";
				qry += "OrderDetailProduct AS Odp ON(RTRIM((Od.OrderCode) = RTRIM(Odp.OrderCode)) AND(RTRIM(Odp.ProductCode) = RTRIM(Od.ProductCode)) AND odp.Status = 0) INNER JOIN ";
				qry += "Product AS Prod ON RTRIM(Od.ProductCode) = RTRIM(Prod.Code) ";
				qry += myList.Count() > 1 ? "WHERE Ord.code IN ('" + string.Join("','", myList) + "')" : "where Ord.code = '" + myList[0] + "'";
				qry += " GROUP BY Od.Id, Ord.Id, Ord.Code, Ord.Description, Ord.ProviderCode, Pro.Name, Pro.Barcode, Od.ProductCode, Prod.Id, Prod.Description, ";
				qry += "Prod.BarCode, Od.Quantity";

				List<ViewOrder> viewOrderList = await conn.QueryAsync<ViewOrder>(qry);
				return viewOrderList;
			}
			catch (Exception ex1)
			{
				Exception ex = ex1;
				return (List<ViewOrder>)null;
			}
		}
	}
}
