using ShipBase.DAL.SectionOne.Interfaces;
using ShipBase.Domain.SectionOne.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBase.DAL.SectionOne.Repositories
{
    public class CustomerRepository : IBaseRepository<Customer>
    {

        private readonly ApplicationDbContext _db;

        public CustomerRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task Create(Customer entity)
        {
            await _db.Customers.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Customer entity)
        {
            _db.Customers.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Customer> GetAll()
        {
            return _db.Customers;
        }

        public async Task<Customer> Update(Customer entity)
        {
            _db.Customers.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
