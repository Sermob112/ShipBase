using ShipBase.DAL.SectionOne.Interfaces;
using ShipBase.Domain.SectionOne.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBase.DAL.SectionOne.Repositories
{
    public class PurchRepository : IBaseRepository<Purch>
    {
        private readonly ApplicationDbContext _db;

        public PurchRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task Create(Purch entity)
        {
            await _db.Purches.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Purch entity)
        {
            _db.Purches.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Purch> GetAll()
        {
            return _db.Purches;
        }

        public async Task<Purch> Update(Purch entity)
        {
            _db.Purches.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
