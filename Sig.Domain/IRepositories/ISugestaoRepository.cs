using Sig.Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sig.Domain.IRepositories
{
    public interface ISugestaoRepository : IRepositoryBase<Sugestao>
    {
        IList<Object> ListarSugestoes(string nomeEmpresa);
        IList<Object> ListarSugestoesPorLinha(int idLinha);
    }
}