using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sig.Domain.IRepositories
{
    public interface IRepositoryBase<T>
    {
        void Save(T obj);
        void SaveOrUpdate(T obj);
        void Update(T obj);
        void Delete(T obj);
        void UpdateClear(T obj);
        T Find(Object id);
        IList<T> List(String order);
        void Delete(object id);
        int ExecuteNonQuery(string cmdText);
        void Flush();
        void Merge(Object obj);
        void Clear();
    }
}
