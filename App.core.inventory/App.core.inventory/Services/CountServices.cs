using System;
using System.Collections.Generic;
using System.Text;

namespace App.core.inventory.Services
{
	public class CountServices
	{
		public List<CountPlan> Items { get; private set; }

		public async Task<List<CountPlan>> GetAll()
		{
			CountPlanRepository _repository = App.Core.Inventory.App.CountPlanRepo;
			this.Items = new List<CountPlan>();
			try
			{
				List<CountPlan> countPlanList = await _repository.GetAllAsync();
				this.Items = countPlanList;
				countPlanList = (List<CountPlan>)null;
			}
			catch (Exception ex)
			{
				this.Items = (List<CountPlan>)null;
				Debug.WriteLine("\t\t\t\tERROR {0}", ex.Message);
			}
			return this.Items;
		}

		public async Task<List<ListItems>> GetCountByIdPage(int id)
		{
			CountPlanRepository _repository = App.Core.Inventory.App.CountPlanRepo;
			List<ListItems> plan = new List<ListItems>();
			try
			{
				List<ListItems> listItemsList = await _repository.GetByIdPage(id);
				plan = listItemsList;
				listItemsList = (List<ListItems>)null;
			}
			catch (Exception ex)
			{
				plan = (List<ListItems>)null;
				Debug.WriteLine("\t\t\t\tERROR {0}", ex.Message);
			}
			return plan;
		}

		public async Task<List<ViewCountPlanDetailItem>> GetByPlanAndProduct(int id, string product)
		{
			CountPlanRepository _repository = App.Core.Inventory.App.CountPlanRepo;
			List<ViewCountPlanDetailItem> plan = new List<ViewCountPlanDetailItem>();
			try
			{
				List<ViewCountPlanDetailItem> countPlanDetailItemList = await _repository.GetByPlanAndProduct(id, product);
				plan = countPlanDetailItemList;
				countPlanDetailItemList = (List<ViewCountPlanDetailItem>)null;
			}
			catch (Exception ex)
			{
				plan = (List<ViewCountPlanDetailItem>)null;
				Debug.WriteLine("\t\t\t\tERROR {0}", ex.Message);
			}
			return plan;
		}

		public async Task<bool> SaveDetail(List<CountPlanDetailItem> item)
		{
			CountPlanRepository _repository = App.Core.Inventory.App.CountPlanRepo;
			bool result = false;
			try
			{
				bool flag = await _repository.Save(item);
				result = flag;
			}
			catch (Exception ex)
			{
				result = false;
				Debug.WriteLine("\t\t\t\tERROR {0}", ex.Message);
			}
			return result;
		}

		public async Task<bool> SaveCountPlan(CountPlan item)
		{
			bool result = false;
			try
			{
				CountPlanRepository _repository = App.Core.Inventory.App.CountPlanRepo;
				bool flag = await _repository.UpdatePlan(item);
				result = flag;
				_repository = (CountPlanRepository)null;
			}
			catch (Exception ex1)
			{
				Exception ex = ex1;
				result = false;
				throw;
			}
			return result;
		}
	}
}
