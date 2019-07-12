using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAPI.Repository
{
    public interface IRepository<T> where T : class
    {
        T Add(T entity);
        T Find(int id);
        ICollection<T> FindAll();
        void Remove(int id);
        void Update(T entity);

    }
}
