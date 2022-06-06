using site.DAL.Entities;
using site.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace site.DAL.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<OrderModel>> GetAll();
        Task<Order> GetById(int id);
        Task<OrderModel> GetModelById(int id);
        Task Create(int userId, List<OrderProductIdModel> products);
        Task<bool> UpdateStatus(int id, string newStatus);
        Task<bool> Delete(int id);
    }
}
