using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using App.core.inventory.Droid.Assets;
using App.core.inventory.Models;
using App.core.inventory.Models.DbModels;

namespace App.core.inventory.Services
{
	public class OrderService
	{
		public List<OrderTShirt> Items { get; private set; }

		public async Task<List<OrderTShirt>> GetOrderProviders(string codeProvider)
		{
			List<OrderTShirt> items = new List<OrderTShirt>();
			OrderRepository _repositoryOrder = App.OrderRepo;
			OrderDetailProductRepository _repositoryOrderDetailProduct = App.OrderDetailProductRepo;
			try
			{
				List<OrderTShirt> orderTshirtList = await _repositoryOrder.GetOrdersByProviderCode(codeProvider);
				List<OrderTShirt> list = orderTshirtList;
				orderTshirtList = (List<OrderTShirt>)null;
				if (list.Count > 0)
				{
					foreach (OrderTShirt orderTshirt in list)
					{
						OrderTShirt row = orderTshirt;
						Task<OrderDetailProduct> exist = _repositoryOrderDetailProduct.GetOrderDetailProduct(row.Code);
						items.Add(new OrderTShirt()
						{
							Code = row.Code,
							Description = row.Description,
							IsSelected = exist != null,
							Value1 = row.Value1
						});
						exist = (Task<OrderDetailProduct>)null;
						row = (OrderTShirt)null;
					}
					return items;
				}
				list = (List<OrderTShirt>)null;
			}
			catch (Exception ex1)
			{
				Exception ex = ex1;
				items = (List<OrderTShirt>)null;
			}
			return this.Items;
		}

		public async Task<List<ViewOrder>> GetOrdersDetails(List<OrderTShirt> items)
		{
			OrderRepository _repositoryOrder = App.OrderRepo;
			List<ViewOrder> viewOrderList;
			try
			{
				List<ViewOrder> listOrder = await _repositoryOrder.GetListOrder(items);
				viewOrderList = listOrder;
			}
			catch (Exception ex)
			{
				throw;
			}
			return viewOrderList;
		}

		public async Task<List<OrderDetailProduct>> GetOrderDetailProductByCode(List<OrderDetailProduct> items)
		{
			OrderDetailProductRepository _repositoryOrderDet = App.OrderDetailProductRepo;
			List<OrderDetailProduct> orderDetailProductList;
			try
			{
				List<OrderDetailProduct> detailProductByCode = await _repositoryOrderDet.GetOrderDetailProductByCode(items);
				orderDetailProductList = detailProductByCode;
			}
			catch (Exception ex)
			{
				throw;
			}
			return orderDetailProductList;
		}

		public async Task<OrderDetailProduct> Save(OrderDetailProduct items)
		{
			OrderDetailProductRepository _repositoryOrderDet = App.OrderDetailProductRepo;
			OrderDetailProduct orderDetailProduct1;
			try
			{
				OrderDetailProduct orderDetailProduct = await _repositoryOrderDet.Add(items);
				orderDetailProduct1 = orderDetailProduct;
			}
			catch (Exception ex)
			{
				throw;
			}
			return orderDetailProduct1;
		}
	}
}
