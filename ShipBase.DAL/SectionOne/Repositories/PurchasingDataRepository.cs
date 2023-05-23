
using ShipBase.DAL.SectionOne.Interfaces;
using ShipBase.Domain.SectionOne.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipBase.DAL.SectionOne.Interfaces;
using ShipBase.Domain.SectionOne.Entity;
using Microsoft.EntityFrameworkCore;

namespace ShipBase.DAL.SectionOne.Repositories
{
    public class PurchasingDataRepository : IBaseRepository<PurchasingData>
    {
        private readonly ApplicationDbContext _db;

        public PurchasingDataRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task Create(PurchasingData entity)
        {
            await _db.PurchasingDatas.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(PurchasingData entity)
        {
            _db.PurchasingDatas.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<PurchasingData> GetAll()
        {
            return _db.PurchasingDatas;
        }

        public async Task<PurchasingData> Update(PurchasingData entity)
        {
            _db.PurchasingDatas.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
