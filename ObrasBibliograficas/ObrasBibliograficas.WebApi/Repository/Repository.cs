using Microsoft.EntityFrameworkCore;
using NetCoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAPI.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        Context _context;
        DbSet<T> _DbSet;
        public Repository(Context context)
        {
            _context = context;
            _DbSet = _context.Set<T>();
        }
        public T Add(T entity)
        {
            _DbSet.Add(entity);
            Commit();
            return entity;
        }

        public T Find(int id)
        {
           return _DbSet.Find(id);
        }

        public ICollection<T> FindAll()
        {
            return _DbSet.ToList();
        }

        public void Remove(int id)
        {
            var reg = _DbSet.Find(id);
            _DbSet.Remove(reg);
            Commit();
            
        }

        public void Update(T entity)
        {
            _DbSet.Update(entity);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
