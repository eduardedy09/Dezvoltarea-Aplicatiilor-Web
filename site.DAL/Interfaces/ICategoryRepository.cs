using site.DAL.Entities;
using site.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace site.DAL.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<CategoryModel>> GetAll();
        Task<Category> GetById(int id);
        Task Create(string name);
        Task<bool> Update(int id, string newName);
        Task<bool> Delete(int id);
        Task<List<ProductModel>> GetAllProducts(int id);
    }
}
