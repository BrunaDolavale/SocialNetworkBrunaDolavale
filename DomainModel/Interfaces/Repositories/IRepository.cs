using DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Interfaces.Repositories
{
    public interface IRepository<T> where T:EntityBase
    {
        void Add(T entity);
        IEnumerable<T> GetAll();
        T Get(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
