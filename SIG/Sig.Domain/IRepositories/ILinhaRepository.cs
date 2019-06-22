using Sig.Domain.Classes;
using Sig.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sig.Domain.IRepositories
{
    public interface ILinhaRepository: IRepositoryBase<Linha>
    {
        IList<Object> ListarLinhasEmpresa(string nomeEmpresa);
        IList<Object> PesquisarLinhas(string filtro);
        Object GetLinha(int idLinha);
        IList<Linha> LinhasPorTipo(int idTipo);
        IList<Linha> LinhasPorEmpresa(int idEmpresa);
    }
}
