using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Data;
using Microsoft.Data.Entity;
using SIT.Data;
using SIT.Data.Interfaces;

namespace SIT.Data.Repositories
{
    public abstract class AbstractRepository<T, TIdentificator> : IRepository<T, TIdentificator> where T : class, IDentificatable<TIdentificator>
    {
        internal ApplicationDbContext context;
        internal DbSet<T> dbSet;

        public AbstractRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public virtual IQueryable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }

        public virtual IQueryable<T> GetById(TIdentificator id, Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            return this.Get().Where(p => p.Id.Equals(id));
        }

        public virtual void Insert(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(TIdentificator id)
        {
            T entityToDelete = this.GetById(id).FirstOrDefault();
            Delete(entityToDelete);
        }

        public virtual void Delete(T entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(T entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual int Count()
        {
            return dbSet.Count();
        }
    }
}
