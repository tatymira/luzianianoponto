﻿using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sig.Infra.__Base
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void BindCurrentSessionContext();
        void Execute();
        ISession CurrentSession { get; }
        void Dispose();
    }
}
