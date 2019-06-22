using NHibernate.Linq;
using Sig.Domain.Classes;
using Sig.Domain.Enum;
using Sig.Domain.IRepositories;
using Sig.Infra.__Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sig.Infra._Repository
{
    public class SugestaoRepository : RepositoryBase<Sugestao>, ISugestaoRepository
    {
        private readonly IEmpresaRepository _empresaRep;

        public SugestaoRepository(IUnitOfWork uow,
            IEmpresaRepository empresaRep)
            : base(uow)
        {
            _empresaRep = empresaRep;
        }

        public IList<Object> ListarSugestoes(string nomeEmpresa)
        {
            Empresa empresa = _empresaRep.GetEmpresaPorRazaoSocial(nomeEmpresa);
            var query = _session.Query<Sugestao>().Where(x => x.Alocacao == empresa.Perfil && x.Ativo);
            if (empresa.Id != 1)
            {
                query = query.Where(x => x.Linha.Empresa.RazaoSocial == empresa.RazaoSocial);
            }
            return query.Select(x => Sugestao.Select(x)).ToList();
        }
        public IList<Object> ListarSugestoesPorLinha(int idLinha)
        {
            return _session.Query<Sugestao>().Where(x => x.Linha.Id == idLinha).ToList<Object>();

        }

    }
}