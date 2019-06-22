using Sig.Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sig.Domain.IRepositories
{
    public interface IParadaRepository : IRepositoryBase<Parada>
    {
        IList<Parada> ListarTodasParadas();
    }
}
