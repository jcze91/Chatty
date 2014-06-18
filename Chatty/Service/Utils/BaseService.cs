﻿using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Utils
{
    /// <summary>
    /// Abstract class for business management
    /// </summary>
    /// <typeparam name="K">table id key type</typeparam>
    /// <typeparam name="E">dbo entity type</typeparam>
    /// <typeparam name="D">dao type</typeparam>
    public abstract class BaseService<K, E, D> : IRepository<K, E>
        where K : struct, IEquatable<K>
        where E : BaseModel<K>, new()
        where D : BaseDao<K, E>, new()
    {

        private D dao { get { return App.container.Resolve<D>(); } }

        public virtual E GetById(K id)
        {
            return dao.GetById(id);
        }

        public virtual E Insert(E entity)
        {
            return dao.Insert(entity);
        }

        public virtual bool Delete(K id)
        {
            return dao.Delete(id);
        }

        public virtual E Update(E entity)
        {
            return dao.Update(entity);
        }

        public virtual IEnumerable<E> SearchFor(Func<E, bool> predicate)
        {
            return dao.SearchFor(predicate);
        }

        public virtual IEnumerable<E> GetAll()
        {
            return dao.GetAll();
        }
    }
}
