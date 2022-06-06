using Microsoft.EntityFrameworkCore;
using site.DAL.Entities;
using site.DAL.Interfaces;
using site.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace site.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task Create(int userId, List<OrderProductIdModel> products)
        {
            var order = new Order
            {
                UserId = userId,
                Date = DateTime.Now,
                Status = "received"
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            foreach (var product in products)
            {
                await _context.OrderProducts.AddAsync(new OrderProduct
                {
                    OrderId = order.Id,
                    ProductId = product.ProductId,
                    Quantity = product.Quantity
                });
            }

            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var order = await GetById(id);

            if (order != default)
            {
                var orderProducts = await _context
                    .OrderProducts
                    .Where(x => x.OrderId == id)
                    .ToListAsync();

                foreach (var orderProduct in orderProducts)
                {
                    _context.OrderProducts.Remove(orderProduct);
                }

                _context.SaveChanges();

                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<OrderModel>> GetAll()
        {
            var orders = await _context
                .Orders
                .Select(x => x.Id)
                .ToListAsync();

            var list = new List<OrderModel>();

            foreach (var order in orders)
            {
                list.Add(await GetModelById(order));
            }

            return list;
        }

        public async Task<Order> GetById(int id)
        {
            return await _context
                .Orders
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<OrderModel> GetModelById(int id)
        {
            var order = await GetById(id);
            var user = await _context
                .Orders
                .Where(x => x.Id == id)
                .Select(x => x.User.Email)
                .FirstOrDefaultAsync();

            var orderProducts = await _context
                .OrderProducts
                .Where(x => x.OrderId == id)
                .Select(x => new
                {
                    productId = x.ProductId,
                    quantity = x.Quantity
                })
                .ToListAsync();

            var list = new List<OrderProductModel>();

            foreach (var orderProduct in orderProducts)
            {
                var product = _context
                    .Products
                    .Where(x => x.Id == orderProduct.productId)
                    .Select(x => new ProductModel
                    {
                        Name = x.Name,
                        Description = x.Description,
                        Category = x.Category.Name,
                        Image = x.Image,
                        Price = x.Price
                    })
                    .FirstOrDefault();

                list.Add(new OrderProductModel
                {
                    Product = product,
                    Quantity = orderProduct.quantity
                });
            }

            return new OrderModel
            {
                User = user,
                Date = order.Date,
                Status = order.Status,
                Products = list
            };
        }

        public async Task<bool> UpdateStatus(int id, string newStatus)
        {
            var order = await GetById(id);

            if (order != default)
            {
                order.Status = newStatus;

                _context.Entry(order).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
