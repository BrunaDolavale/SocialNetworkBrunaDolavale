using Data.Context;
using DomainModel.Entities;
using DomainModel.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public abstract class RepositoryBase<T> :
        IRepository<T> where T : EntityBase
    {
        private SocialNetworkContext _context;
        private DbSet<T> _dbSet;
        public RepositoryBase(SocialNetworkContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public T Get(T entity)
        {
            return _dbSet.Find(entity.Id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet;
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
