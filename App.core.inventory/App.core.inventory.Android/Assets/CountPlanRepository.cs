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
	public class CountPlanRepository
	{
		private readonly SQLiteAsyncConnection conn;

		public string MsgError { get; set; }

		public CountPlanRepository(string dbPath)
		{
			this.conn = new SQLiteAsyncConnection(dbPath, true);
			this.conn.CreateTableAsync<CountPlan>(CreateFlags.None).Wait();
			this.conn.CreateTableAsync<CountPlanDetail>(CreateFlags.None).Wait();
			this.conn.CreateTableAsync<CountPlanDetailItem>(CreateFlags.None).Wait();
			this.conn.CreateTableAsync<Product>(CreateFlags.None).Wait();
		}

		public async Task<List<CountPlan>> GetAllAsync()
		{
			List<CountPlan> countPlanList = await this.conn.Table<CountPlan>().Where(a => a.Status == "2").ToListAsync();
			List<CountPlan> x = countPlanList;
			countPlanList = (List<CountPlan>)null;
			return x;
		}

		public async Task AddNewCountPlanAsync(CountPlan countPlan)
		{
			try
			{
				if (countPlan == null)
					throw new Exception("Count null");
				int num = await this.conn.InsertAsync(countPlan);
				int result = num;
			}
			catch (Exception ex)
			{
				this.MsgError = ex.Message.ToString();
			}
		}

		public async Task<List<ViewCountPlanDetail>> GetById(int Id)
		{
			List<ViewCountPlanDetail> list = new List<ViewCountPlanDetail>();
			try
			{
				string qry = "SELECT CPD.Id AS Id,";
				qry += "CP.Id AS IdCountPlan,";
				qry += "CP.Name,";
				qry += "CP.Description,";
				qry += "CPD.ProductCode,";
				qry += "CPD.Quantity,";
				qry += "CPD.TotalCounted,";
				qry += "P.Barcode,";
				qry += "P.Description AS ProductDescription,";
				qry += "CASE  WHEN SUM(CPDI.Quantity) > 0 THEN  SUM(CPDI.Quantity) ELSE 0 END AS TotalProduct,";
				qry += "CP.Warehouse ";
				qry += " FROM CountPlan CP INNER JOIN";
				qry += " CountPlanDetail CPD ON  CP.Id = CPD.CountPlanId INNER JOIN";
				qry += " Product P ON CPD.ProductCode = P.Code LEFT OUTER JOIN";
				qry += " CountPlanDetailItem CPDI ON ((CPD.CountPlanId = CPDI.CountPlanId) AND (P.Code = CPDI.ProductCode ))";
				qry = qry + " WHERE CP.Id = '" + (object)Id + "' ";
				qry += " GROUP BY CP.Id, CP.Name, CP.Description, CPD.ProductCode, CPD.Quantity,  CPD.TotalCounted, P.Barcode, P.Description";
				List<ViewCountPlanDetail> viewCountPlanDetailList = await this.conn.QueryAsync<ViewCountPlanDetail>(qry, Array.Empty<object>());
				return viewCountPlanDetailList;
			}
			catch (Exception ex)
			{
				this.MsgError = ex.Message.ToString();
			}
			return (List<ViewCountPlanDetail>)null;
		}

		public async Task AddCountPlanDetailItem(List<CountPlanDetailItem> items)
		{
			try
			{
				if (items == null)
					throw new Exception("Count null");
				int num = await this.conn.InsertAsync((object)items);
				int result = num;
			}
			catch (Exception ex)
			{
				this.MsgError = ex.Message.ToString();
			}
		}

		public async Task<List<ListItems>> GetByIdPage(int id)
		{
			List<ListItems> listItemsList1;
			try
			{
				string qry = "SELECT CPD.Id AS Id,";
				qry += "CP.Id AS IdCountPlan,";
				qry += "CP.Name,";
				qry += "CP.Description,";
				qry += "CPD.ProductCode,";
				qry += "CPD.Quantity,";
				qry += "CPD.TotalCounted,";
				qry += "P.Barcode,";
				qry += "P.Description AS ProductDescription,";
				qry += "CASE  WHEN SUM(CPDI.Quantity) > 0 THEN  SUM(CPDI.Quantity) ELSE 0 END AS TotalProduct,";
				qry += "CP.Warehouse ";
				qry += " FROM CountPlan CP INNER JOIN";
				qry += " CountPlanDetail CPD ON  CP.Id = CPD.CountPlanId INNER JOIN";
				qry += " Product P ON CPD.ProductCode = P.Code LEFT OUTER JOIN";
				qry += " CountPlanDetailItem CPDI ON ((CPD.CountPlanId = CPDI.CountPlanId) AND (P.Code = CPDI.ProductCode ))";
				qry = qry + " WHERE CP.Id = '" + (object)id + "' ";
				qry += " GROUP BY CP.Id, CP.Name, CP.Description, CPD.ProductCode, CPD.Quantity,  CPD.TotalCounted, P.Barcode, P.Description";
				List<ListItems> listItemsList = await this.conn.QueryAsync<ListItems>(qry, Array.Empty<object>());
				listItemsList1 = listItemsList;
			}
			catch (Exception ex1)
			{
				Exception ex = ex1;
				throw;
			}
			return listItemsList1;
		}

		public async Task<List<ViewCountPlanDetailItem>> GetByPlanAndProduct(int plan, string product)
		{
			List<ViewCountPlanDetailItem> items = new List<ViewCountPlanDetailItem>();
			ViewCountPlanDetailItem details = new ViewCountPlanDetailItem();
			string qry = "SELECT CP.Id, CP.CountPlanId, CP.UserCode, CP.DateCreated, CP.Quantity, CP.ProductCode, CP.Count, PR.Description ";
			qry += " FROM countPlanDetailItem CP INNER JOIN ";
			qry += " Product PR ON  CP.ProductCode = PR.Code";
			qry = qry + " WHERE CP.CountPlanId = '" + (object)plan + "' AND CP.ProductCode = '" + product + "'";
			List<ViewCountPlanDetailItem> countPlanDetailItemList = await this.conn.QueryAsync<ViewCountPlanDetailItem>(qry, Array.Empty<object>());
			List<ViewCountPlanDetailItem> list = countPlanDetailItemList;
			countPlanDetailItemList = (List<ViewCountPlanDetailItem>)null;
			if (!list.Any<ViewCountPlanDetailItem>())
			{
				list = new List<ViewCountPlanDetailItem>();
				string qry2 = "SELECT pro.Id, pro.Code, pro.BarCode, pro.Description, pro.Value1, pro.Value2, pro.Value3, pro.Value4, pro.Value5 from CountPlanDetail cpd INNER JOIN Product pro ON cpd.ProductCode = pro.Code ";
				qry2 = qry2 + " WHERE cpd.CountPlanId ='" + (object)plan + "' AND cpd.ProductCode ='" + product + "'";
				List<Product> productList = await this.conn.QueryAsync<Product>(qry2, Array.Empty<object>());
				List<Product> descriptionProduct = productList;
				productList = (List<Product>)null;
				if (descriptionProduct != null)
					list.Add(new ViewCountPlanDetailItem()
					{
						CountPlanId = plan,
						ProductCode = product,
						Quantity = 0,
						Description = descriptionProduct.FirstOrDefault<Product>().Description
					});
				else
					list = (List<ViewCountPlanDetailItem>)null;
				qry2 = (string)null;
				descriptionProduct = (List<Product>)null;
			}
			return list;
		}

		public async Task AddCountPlanDetail(CountPlanDetail items)
		{
			try
			{
				if (items == null)
					throw new Exception("Count null");
				int num = await this.conn.InsertAsync((object)items);
				int result = num;
			}
			catch (Exception ex)
			{
				this.MsgError = ex.Message.ToString();
			}
		}

		public async Task AddProduct(Product items)
		{
			try
			{
				if (items == null)
					throw new Exception("Count null");
				int num = await this.conn.InsertAsync((object)items);
				int result = num;
			}
			catch (Exception ex)
			{
				this.MsgError = ex.Message.ToString();
			}
		}

		public async Task<bool> Save(List<CountPlanDetailItem> items)
		{
			bool result = false;
			try
			{
				if (items == null)
					return false;
				foreach (CountPlanDetailItem countPlanDetailItem in items)
				{
					CountPlanDetailItem row = countPlanDetailItem;
					row.DateCreated = DateTime.Now.ToString("dd/MM/yyyy hh:mm");
					int num = await this.conn.InsertAsync((object)items);
					row = (CountPlanDetailItem)null;
				}
				result = true;
			}
			catch (Exception ex)
			{
				this.MsgError = ex.Message.ToString();
				return false;
			}
			return result;
		}

		public async Task<bool> UpdatePlan(CountPlan item)
		{
			bool result = false;
			int success = 0;
			try
			{
				int planId = item.Id;
				CountPlan countPlan1 = await this.conn.Table<CountPlan>().Where((Expression<Func<CountPlan, bool>>)(a => a.Id == item.Id)).FirstOrDefaultAsync();
				CountPlan getForUpdate = countPlan1;
				countPlan1 = (CountPlan)null;
				CountPlanDetail planDetails = new CountPlanDetail();
				int newPlanId = 0;
				if (getForUpdate != null)
				{
					string planName = getForUpdate.Name;
					string planDescription = getForUpdate.Description;
					string warehouse = getForUpdate.Warehouse;
					getForUpdate.Status = "0";
					getForUpdate.UserUpdated = item.UserUpdated;
					CountPlan countPlan3 = getForUpdate;
					DateTime now = DateTime.Now;
					string str1 = now.ToString("dd/MM/yyyy hh:mm");
					countPlan3.DateUpdated = str1;
					int num1 = await this.conn.UpdateAsync((object)getForUpdate);
					bool createNewPlan = Convert.ToBoolean(item.Value2);
					if (createNewPlan)
					{
						CountPlan countPlan2 = await this.conn.Table<CountPlan>().OrderByDescending<int>((Expression<Func<CountPlan, int>>)(a => a.Id)).FirstAsync();
						CountPlan lastOrDefault = countPlan2;
						countPlan2 = (CountPlan)null;
						CountPlan countPlan4 = new CountPlan();
						countPlan4.Warehouse = warehouse;
						countPlan4.Name = "Plan Conteo " + planName;
						countPlan4.Description = "Productos no contados, " + planDescription;
						countPlan4.Status = "2";
						now = DateTime.Now;
						countPlan4.DateCreated = now.ToString("dd/MM/yyyy hh:mm");
						CountPlan countplan = countPlan4;
						int num = await this.conn.InsertAsync((object)countplan);
						success = num;
						newPlanId = countplan.Id;
						if (success > 0)
						{
							string qry = "SELECT CPD.Id AS Id,";
							qry += "CP.Id AS IdCountPlan,";
							qry += "CP.Name,";
							qry += "CP.Description,";
							qry += "CPD.ProductCode,";
							qry += "CPD.Quantity,";
							qry += "CPD.TotalCounted,";
							qry += "P.Barcode,";
							qry += "P.Description AS ProductDescription,";
							qry += "CASE  WHEN SUM(CPDI.Quantity) > 0 THEN  SUM(CPDI.Quantity) ELSE 0 END AS TotalProduct,";
							qry += "CP.Warehouse ";
							qry += " FROM CountPlan CP INNER JOIN";
							qry += " CountPlanDetail CPD ON  CP.Id = CPD.CountPlanId INNER JOIN";
							qry += " Product P ON CPD.ProductCode = P.Code LEFT OUTER JOIN";
							qry += " CountPlanDetailItem CPDI ON ((CPD.CountPlanId = CPDI.CountPlanId) AND (P.Code = CPDI.ProductCode ))";
							qry = qry + " WHERE CP.Id = '" + (object)planId + "' ";
							qry += " GROUP BY CP.Id, CP.Name, CP.Description, CPD.ProductCode, CPD.Quantity,  CPD.TotalCounted, P.Barcode, P.Description";
							List<ViewCountPlanDetail> viewCountPlanDetailList = await this.conn.QueryAsync<ViewCountPlanDetail>(qry, Array.Empty<object>());
							List<ViewCountPlanDetail> list = viewCountPlanDetailList;
							viewCountPlanDetailList = (List<ViewCountPlanDetail>)null;
							if (list != null)
							{
								foreach (ViewCountPlanDetail viewCountPlanDetail in list)
								{
									ViewCountPlanDetail row = viewCountPlanDetail;
									planDetails.CountPlanId = newPlanId;
									planDetails.ProductCode = row.ProductCode;
									planDetails.Quantity = row.Quantity - row.TotalProduct;
									CountPlanDetail countPlanDetail = planDetails;
									now = DateTime.Now;
									string str2 = now.ToString("dd/MM/yyyy hh:mm");
									countPlanDetail.DateCreated = str2;
									planDetails.UserIdCreated = 0;
									if (planDetails.Quantity > 0)
									{
										int num2 = await this.conn.InsertAsync((object)planDetails);
									}
									row = (ViewCountPlanDetail)null;
								}
							}
							qry = (string)null;
							list = (List<ViewCountPlanDetail>)null;
						}
						lastOrDefault = (CountPlan)null;
						countplan = (CountPlan)null;
					}
					planName = (string)null;
					planDescription = (string)null;
					warehouse = (string)null;
				}
				result = true;
				getForUpdate = (CountPlan)null;
				planDetails = (CountPlanDetail)null;
			}
			catch (Exception ex)
			{
				result = false;
				throw;
			}
			return result;
		}
	}
}
