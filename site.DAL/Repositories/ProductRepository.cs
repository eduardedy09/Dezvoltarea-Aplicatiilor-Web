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
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(string name, string description, string image, decimal price, int categoryId)
        {
            var product = new Product
            {
                Name = name,
                Description = description,
                Image = image,
                Price = price,
                CategoryId = categoryId
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var product = await GetById(id);

            if (product != default)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<ProductModel>> GetAll()
        {
            return await _context
                .Products
                .Include(x => x.Category)
                .Select(x => new ProductModel
                {
                    Name = x.Name,
                    Description = x.Description,
                    Image = x.Image,
                    Price = x.Price,
                    Category = x.Category.Name
                })
                .ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _context
                .Products
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<ProductModel> GetModelById(int id)
        {
            return await _context
                .Products
                .Where(x => x.Id == id)
                .Include(x => x.Category)
                .Select(x => new ProductModel
                {
                    Name = x.Name,
                    Description = x.Description,
                    Image = x.Image,
                    Price = x.Price,
                    Category = x.Category.Name
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateDescription(int id, string newDescription)
        {
            var product = await GetById(id);

            if (product != default)
            {
                product.Description = newDescription;

                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateImage(int id, string newImage)
        {
            var product = await GetById(id);

            if (product != default)
            {
                product.Image = newImage;

                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdatePrice(int id, decimal newPrice)
        {
            var product = await GetById(id);

            if (product != default)
            {
                product.Price = newPrice;

                _context.Entry(product).State = EntityState.Modified;
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
