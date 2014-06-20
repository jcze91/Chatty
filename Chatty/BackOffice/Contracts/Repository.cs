using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace BackOffice.Contracts
{
    [ServiceContract]
    public interface IRepository<K, E>
        where K : struct, IEquatable<K>
        where E : Utils.BaseEntity<K>
    {
        [OperationContract]
        E GetById(K id);

        [OperationContract]
        E Insert(E entity);
        [OperationContract]
        bool Delete(K id);
        [OperationContract]
        E Update(E entity);

        [OperationContract]
        IEnumerable<E> SearchFor(Func<E, bool> predicate);
        [OperationContract]
        IEnumerable<E> GetAll();
    }
}