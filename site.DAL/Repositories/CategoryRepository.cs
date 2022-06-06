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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(string name)
        {
            var category = new Category
            {
                Name = name
            };

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var category = await GetById(id);

            if (category != default)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<CategoryModel>> GetAll()
        {
            var categories = _context
                .Categories
                .Select(x => x.Name)
                .ToList();
            var list = new List<CategoryModel>();

            foreach (var category in categories)
            {
                list.Add(new CategoryModel
                {
                    Name = category
                });
            }

            return list;
        }

        public async Task<List<ProductModel>> GetAllProducts(int id)
        {
            return await _context
                .Products
                .Where(x => x.CategoryId == id)
                .Select(x => new ProductModel
                {
                    Name = x.Name,
                    Description = x.Description,
                    Category = x.Category.Name,
                    Image = x.Image,
                    Price = x.Price
                })
                .ToListAsync();
        }

        public async Task<Category> GetById(int id)
        {
            return _context
                .Categories
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }

        public async Task<bool> Update(int id, string newName)
        {
            var category = await GetById(id);

            if (category != default)
            {
                category.Name = newName;

                _context.Entry(category).State = EntityState.Modified;
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
