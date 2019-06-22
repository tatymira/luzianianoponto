using Sig.Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sig.Domain.IRepositories
{
    public interface ITipoRepository : IRepositoryBase<Tipo>
    {
        Object ListarTipos();
    }
}