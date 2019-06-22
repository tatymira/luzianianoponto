
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sig.Infra.__Base
{
    public class RepositoryBase<T> : IRepositoryBase<T>
    {
        private UnitOfWork _unitOfWork;

        public RepositoryBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
        }
        protected ISession _session { get { return _unitOfWork.CurrentSession; } }


        public void Save(T obj)
        {
            _session.Save(obj);
        }

        public void SaveOrUpdate(T obj)
        {
            _session.SaveOrUpdate(obj);
        }

        public void Update(T obj)
        {
            _session.Update(obj);
        }

        public void UpdateClear(T obj)
        {
            _session.Clear();
            _session.Update(obj);
        }

        public void Delete(T obj)
        {
            _session.Delete(obj);
        }

        public void Delete(object id)
        {
            _session.Delete(Find(id));
        }

        public T Find(object id)
        {
            return _session.Get<T>(id);
        }

        public IList<T> List(String order)
        {
            ICriteria query = _session.CreateCriteria(typeof(T));
            query.AddOrder(new Order(order, true));
            return query.List<T>();
        }

        public void Flush()
        {
            _session.Flush();
        }

        public int ExecuteNonQuery(string cmdText)
        {
            return _session.CreateSQLQuery(cmdText).ExecuteUpdate();
        }

        public void Merge(Object obj)
        {
            _session.Merge(obj);
        }

        public void Clear()
        {
            _session.Clear();
        }

    }
}
