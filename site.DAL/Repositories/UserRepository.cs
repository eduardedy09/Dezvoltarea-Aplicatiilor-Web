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
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete(int id)
        {
            var user = await _context
                .Users
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (user != default)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<UserModel>> GetAll()
        {
            var usersData = await GetAllQuery();
            var list = new List<UserModel>();

            foreach(var user in usersData)
            {
                list.Add(user);
            }

            return list;
        }

        public async Task<IQueryable<UserModel>> GetAllQuery()
        {
            // sau (fara async la metoda)
            //return Task.FromResult(_context.Users.AsQueryable());

            var usersOrders = (
                from u in _context.Users
                join uo in _context.Orders on u.Id equals uo.UserId
                into j

                from result in j.DefaultIfEmpty()
                select new
                {
                    userId = u.Id,
                    email = u.Email,
                    orderId = result.Id
                })
                .GroupBy(x => new
                {
                    id = x.userId,
                    email = x.email
                })
                .Select(x => new
                {
                    userId = x.Key.id,
                    email = x.Key.email,
                    ordersCount = x.Count() // ? counts null values
                });

            var usersData = _context
                .UsersData
                .Join(usersOrders, b => b.UserId, a => a.userId, (b, a) => new UserModel
                {
                    FirstName = b.FirstName,
                    LastName = b.LastName,
                    Email = a.email,
                    Phone = b.Phone,
                    Address = b.Address,
                    City = b.City,
                    County = b.County,
                    Country = b.Country,
                    Zipcode = b.Zipcode,
                    OrdersCount = a.ordersCount
                });

            return usersData;
        }

        public async Task<UserModel> GetById(int id)
        {
            var email = _context
                .Users
                .Where(x => x.Id == id)
                .Select(x => x.Email)
                .FirstOrDefault();

            var users = await GetAllQuery();

            foreach (var user in users)
            {
                if (user.Email == email)
                {
                    return user;
                }
            }

            return default;
        }

        public async Task<List<OrderSummaryModel>> GetUserOrders(int userId)
        {
            var orders = _context
                .Orders
                .Where(x => x.UserId == userId);

            var summaries = await _context
                .OrderProducts
                .Include(x => x.Product)
                .Join(orders, b => b.OrderId, a => a.Id, (b, a) => new
                {
                    date = a.Date,
                    status = a.Status,
                    total = b.Product.Price * b.Quantity
                })
                .GroupBy(x => new
                {
                    date = x.date,
                    status = x.status
                })
                .Select(x => new OrderSummaryModel
                {
                    Date = x.Key.date,
                    Status = x.Key.status,
                    TotalCost = x.Sum(x => x.total)
                })
                .ToListAsync();

            return summaries;
        }

        public async Task<bool> UpdateAddress(int id, string newAddress, string newCity, string newCounty, string newCountry, string newZipcode)
        {
            var userData = await _context
                .UsersData
                .Where(x => x.UserId == id)
                .FirstOrDefaultAsync();

            if (userData != default)
            {
                userData.Address = newAddress;
                userData.City = newCity;
                userData.County = newCounty;
                userData.Country = newCountry;
                userData.Zipcode = newZipcode;

                _context.Entry(userData).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdatePhone(int id, string newPhone)
        {
            var userData = await _context
                .UsersData
                .Where(x => x.UserId == id)
                .FirstOrDefaultAsync();

            if (userData != default)
            {
                userData.Phone = newPhone;

                _context.Entry(userData).State = EntityState.Modified;
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
