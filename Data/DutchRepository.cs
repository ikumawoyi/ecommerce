using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
	public class DutchRepository : IDutchRepository
	{
		private readonly DutchContext _ctx;
		private readonly ILogger<DutchRepository> _logger;

		public DutchRepository(DutchContext ctx, ILogger<DutchRepository> logger)
		{
			_ctx = ctx;
			_logger = logger;
		}

		public void AddEntity(object model)
		{
			_ctx.Add(model);
		}

		public void AddOrder(Order newOrder)
		{
			foreach (var item in newOrder.Items)
			{
				item.Product = _ctx.Products.Find(item.Product.Id);
			}

			AddEntity(newOrder);
		}

		public IEnumerable<Order> GetAllOrders(bool includeItems)
		{
			if (includeItems)
			{
				return _ctx.Orders
				.Include(n => n.Items)
				.ThenInclude(i => i.Product) 
				.ToList();
			}
			else
			{
				return _ctx.Orders
				.ToList();
			}
			
		}

		public IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems)
		{
			if (includeItems)
			{
				return _ctx.Orders
					.Where(o => o.User.UserName == username)
					.Include(n => n.Items)
					.ThenInclude(i => i.Product)
					.ToList();
			}
			else
			{
				return _ctx.Orders
					.Where(o => o.User.UserName == username)
					.ToList();
			}
		}

		public IEnumerable<Product> GetAllProducts()
		{
			try
			{
				_logger.LogInformation("GetAllProducts Was called");
				return _ctx.Products
					.OrderBy(p => p.Title)
					.ToList();
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get all products: {ex}");
				return null;
			}
		}

		public Order GetOrderById(string username, int id)
		{
			return _ctx.Orders
				.Include(n => n.Items)
				.ThenInclude(i => i.Product)
				.Where(x => x.Id == id && x.User.UserName == username)
				.FirstOrDefault();
		}

		public IEnumerable<Product> GetProductsByCategory(string category)
		{
			return _ctx.Products
				.Where(p => p.Category == category)
				.ToList();
		}

		public bool SaveAll()
		{
			return _ctx.SaveChanges() > 0;
		}
	}
}
