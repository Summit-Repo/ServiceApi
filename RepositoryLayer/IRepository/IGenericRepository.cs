using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.IRepository
{
   public interface IGenericRepository <TEntity> where TEntity : class
    {
        TEntity GetById(int Id);
        IEnumerable<TEntity> GetAll();

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);


    }
}
