using BackOffice.DataAccess;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BackOffice.Utils
{
    /// <summary>
    /// Abstract class for data-access
    /// </summary>
    /// <typeparam name="K">table id key type</typeparam>
    /// <typeparam name="E">dbo entity type</typeparam>
    public abstract class BaseDao<K, E> : Contracts.IRepository<K, E>
        where K : struct, IEquatable<K>
        where E : BaseEntity<K>, new()
    {

        protected ChattyDbContext ctx { get { return Startup.container.Resolve<ChattyDbContext>(); } }

        public virtual E GetById(K id)
        {
            try
            {
                return ctx.Set<E>().SingleOrDefault(x => x.Id.Equals(id));
            }
            catch
            {
                return null;
            }
        }

        public virtual E Insert(E entity)
        {
            try
            {
                entity.CreatedAt = DateTime.Now;
                entity.UpdatedAt = DateTime.Now;
                ctx.Set<E>().Add(entity);
                ctx.SaveChanges();
                return entity;
            }
            catch
            {
                return null;
            }
        }

        public virtual bool Delete(K id)
        {
            try
            {
                ctx.Entry<E>(GetById(id)).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual E Update(E entity)
        {
            try
            {
                var e = ctx.Set<E>().Find(entity.Id);
                e.UpdatedAt = DateTime.Now;
                ctx.Entry<E>(e).CurrentValues.SetValues(entity);
                ctx.SaveChanges();
                return entity;
            }
            catch
            {
                return null;
            }
        }

        public virtual IEnumerable<E> SearchFor(Func<E, bool> predicate)
        {
            try
            {
                return ctx.Set<E>().Where(predicate).ToArray();
            }
            catch
            {
                return null;
            }
        }

        public virtual IEnumerable<E> GetAll()
        {
            try
            {
                return ctx.Set<E>().ToArray();
            }
            catch
            {
                return null;
            }
        }
    }
}