using Sig.Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sig.Domain.IRepositories
{
    public interface IEmpresaRepository : IRepositoryBase<Empresa>
    {
        Object ListarEmpresas();
        Empresa GetEmpresaPorCnpj(string cnpj);
        Object ValidarLogin(string nome, string senha);
        Empresa GetEmpresaPorRazaoSocial(string razaoSocial);
    }
}
