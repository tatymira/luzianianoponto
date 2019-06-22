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
    public class HorarioRepository : RepositoryBase<Horario>, IHorarioRepository
    {
        public HorarioRepository(IUnitOfWork uow)
            : base(uow)
        {
        }

        public IList<Horario> ListarHorariosPorLinha(int idlinha)
        {
            return _session.Query<Horario>().Where(x => x.Linha.Id == idlinha).ToList();
        }

        public void ExcluirHorariosPorLinha(int idlinha)
        {
            List<Horario> lst = ListarHorariosPorLinha(idlinha).ToList();
            if (lst.Count() > 0)
            {
                foreach (Horario item in lst)
                {
                    Delete(item);
                }
            }
        }
    }
}