using Service.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Utils
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

        public virtual E GetById(K id)
        {
            try
            {
                using (ChattyDbContext ctx = new ChattyDbContext())
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
                using (ChattyDbContext ctx = new ChattyDbContext())
                {
                    ctx.Set<E>().Add(entity);
                    ctx.SaveChanges();
                    return entity;
                }
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
                using (ChattyDbContext ctx = new ChattyDbContext())
                {
                    ctx.Entry<E>(GetById(id)).State = System.Data.Entity.EntityState.Deleted;
                    ctx.SaveChanges();
                }
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
                using (ChattyDbContext ctx = new ChattyDbContext())
                {
                    ctx.Entry<E>(ctx.Set<E>().Find(entity.Id)).CurrentValues.SetValues(entity);
                    ctx.SaveChanges();
                }
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
                using (ChattyDbContext ctx = new ChattyDbContext())
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
                using (ChattyDbContext ctx = new ChattyDbContext())
                    return ctx.Set<E>().ToArray();
            }
            catch
            {
                return null;
            }
        }
    }
}
