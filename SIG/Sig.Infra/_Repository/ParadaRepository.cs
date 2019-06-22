using Sig.Domain.Classes;
using Sig.Domain.IRepositories;
using Sig.Infra.__Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sig.Infra._Repository
{
    public class ParadaRepository : RepositoryBase<Parada>, IParadaRepository
    {
        public ParadaRepository(IUnitOfWork uow)
            : base(uow)
        {
        }

        public IList<Parada> ListarTodasParadas()
        {
            return _session.QueryOver<Parada>().List();
        }
        
    }
}