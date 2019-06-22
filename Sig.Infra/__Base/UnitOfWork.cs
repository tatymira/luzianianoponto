using NHibernate;
using NHibernate.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Sig.Infra.__Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private static readonly ISessionFactory _sessionFactory;
        private ITransaction _transaction;
        public ISession CurrentSession { get; private set; }

        static UnitOfWork()
        {
            _sessionFactory = SessionFactory.GetFactory();
        }

        public UnitOfWork()
        {

            if (!CurrentSessionContext.HasBind(_sessionFactory))
            {
                CurrentSessionContext.Bind(_sessionFactory.OpenSession());
            }

            CurrentSession = _sessionFactory.GetCurrentSession();
            //BeginTransaction

        }
        public void BindCurrentSessionContext()
        {
            CurrentSessionContext.Bind(this.CurrentSession);
        }
        public void BeginTransaction()
        {
            _transaction = CurrentSession.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void Execute()
        {
            try
            {
                if (_transaction.IsActive)
                {
                    _transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                _transaction.Rollback();
                throw ex;
            }
        }
        public void Commit()
        {

            if (_transaction != null && _transaction.IsActive)
                _transaction.Commit();

        }
        public void Rollback()
        {
            if (_transaction != null && _transaction.IsActive)
                _transaction.Rollback();
        }
        public void Dispose()
        {

            CurrentSession = CurrentSessionContext.Unbind(_sessionFactory);
            if (CurrentSession != null)
            {
                CurrentSession.Dispose();
            }
        }

    }
}