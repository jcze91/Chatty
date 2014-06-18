using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Service.Contracts
{
    public interface IRepository<K, E>
        where K : struct, IEquatable<K>
        where E : Utils.BaseModel<K>
    {
        E GetById(K id);

        E Insert(E entity);
        bool Delete(K id);
        E Update(E entity);

        IEnumerable<E> SearchFor(Func<E, bool> predicate);
        IEnumerable<E> GetAll();
    }
}
