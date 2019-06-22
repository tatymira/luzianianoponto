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
    public class EmpresaRepository : RepositoryBase<Empresa>, IEmpresaRepository
    {
        public EmpresaRepository(IUnitOfWork uow)
            : base(uow)
        {
        }

        public Object ListarEmpresas()
        {
            return _session.Query<Empresa>().Where(x => x.Id != 1).OrderBy(x => x.Nome).ToList();
        }

        public Empresa GetEmpresaPorCnpj(string cnpj)
        {
            return _session.Query<Empresa>().Where(x => x.Cnpj == cnpj).ToList().FirstOrDefault();
        }

        public Empresa GetEmpresaPorRazaoSocial(string razaoSocial)
        {
            return _session.Query<Empresa>().Where(x => x.RazaoSocial == razaoSocial).ToList().FirstOrDefault();
        }

        public Object ValidarLogin(string nome, string senha)
        {
            var query = _session.QueryOver<Empresa>()
                .Where(x => x.RazaoSocial == nome && x.Senha == senha).List().FirstOrDefault();


            if (query == null)
            {
                return false;
            }
            else
            {
                return new
                {
                    Validacao = true,
                    Perfil = query.Perfil.ToString()
                };
            }
        }
    }
}