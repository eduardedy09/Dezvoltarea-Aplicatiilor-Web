using site.DAL.Entities;
using site.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace site.DAL.Interfaces
{
    public interface IProductRepository
    {
        Task<List<ProductModel>> GetAll();
        Task<ProductModel> GetModelById(int id);
        Task<Product> GetById(int id);
        Task Create(string name, string description, string image, decimal price, int categoryId);
        Task<bool> UpdateDescription(int id, string newDescription);
        Task<bool> UpdateImage(int id, string newImage);
        Task<bool> UpdatePrice(int id, decimal newPrice);
        Task<bool> Delete(int id);
    }
}
