using ShipBase.DAL.Interfaces;
using ShipBase.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBase.DAL.SectionOne.Repositories
{
    internal class CustomerRepository : IBaseRepository<Сustomer>
    {

        private readonly ApplicationDbContext _db;

        public CustomerRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task Create(Сustomer entity)
        {
            await _db.Customers.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Сustomer entity)
        {
            _db.Customers.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Сustomer> GetAll()
        {
            return _db.Customers;
        }

        public async Task<Сustomer> Update(Сustomer entity)
        {
            _db.Customers.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
