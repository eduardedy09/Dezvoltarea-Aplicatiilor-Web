using site.DAL.Entities;
using site.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace site.DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserModel>> GetAll();
        Task<UserModel> GetById(int id);
        Task<bool> UpdatePhone(int id, string newPhone);
        Task<bool> UpdateAddress(int id, string newAddress, string newCity, string newCounty, string newCountry, string newZipcode);
        Task<bool> Delete(int id);
        Task<IQueryable<UserModel>> GetAllQuery();
        Task<List<OrderSummaryModel>> GetUserOrders(int userId);
    }
}
