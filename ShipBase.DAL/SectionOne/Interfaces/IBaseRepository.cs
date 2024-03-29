﻿using System.Linq;
using System.Threading.Tasks;

namespace ShipBase.DAL.SectionOne.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task Create(T entity);

        IQueryable<T> GetAll();

        Task Delete(T entity);

        Task<T> Update(T entity);
    }
}