using NHibernate.Linq;
using Sig.Domain.Classes;
using Sig.Domain.IRepositories;
using Sig.Infra.__Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sig.Infra._Repository
{
    public class TipoRepository : RepositoryBase<Tipo>, ITipoRepository
    {
        public TipoRepository(IUnitOfWork uow)
            : base(uow)
        {
        }

        public Object ListarTipos()
        {
            return _session.Query<Tipo>().OrderBy(x => x.Descricao).ToList();
        }
        
    }
}