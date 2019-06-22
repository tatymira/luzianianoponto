using Sig.Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sig.Domain.IRepositories
{
    public interface IHorarioRepository : IRepositoryBase<Horario>
    {
        IList<Horario> ListarHorariosPorLinha(int idlinha);
        void ExcluirHorariosPorLinha(int idlinha);
    }
}