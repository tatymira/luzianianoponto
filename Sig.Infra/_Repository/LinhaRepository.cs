
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sig.Infra.__Base;
using Sig.Domain.Classes;
using Sig.Domain.IRepositories;

using NHibernate;
using NHibernate.Linq;
using NHibernate.Criterion;
using NHibernate.Transform;
using Sig.Domain.Dto;

namespace Sig.Infra._Repository
{
    public class LinhaRepository : RepositoryBase<Linha>, ILinhaRepository
    {
        private readonly IHorarioRepository _horarioRep;

        public LinhaRepository(IUnitOfWork uow, IHorarioRepository horarioRep)
            : base(uow)
        {
            _horarioRep = horarioRep;
        }


        public IList<Object> ListarLinhasEmpresa(string nomeEmpresa)
        {
            var linhas = _session.Query<Linha>().Where(x => x.Empresa.RazaoSocial == nomeEmpresa);
            return linhas.Select(x => Linha.SelectAdm(x)).ToList();
        }

        public IList<Object> PesquisarLinhas(string filtro)
        {
            var consulta = filtro.Split('&');
            var linhas = _session.Query<Linha>();
            if (consulta[0] == "Trajeto")
            {
                linhas = linhas.Where(x => x.Nome.ToUpper().Contains(consulta[1].ToUpper())).OrderBy(x => x.Nome);
            }
            if (consulta[0] == "Linha")
            {
                linhas = linhas.Where(x => x.Numero.ToString() == consulta[1]).OrderBy(x => x.Nome);
            }
            if (consulta[0] == "Horarios")
            {
                IList<Linha> lstPorHorarios = new List<Linha>();
                var faixaDeHorarios = consulta[1].Split('-');
                var horarioUm = faixaDeHorarios[0];
                var horarioDois = faixaDeHorarios[1];

                foreach (var linha in linhas)
                {
                    foreach (Horario horario in linha.Horarios)
                    {
                        if (Convert.ToDateTime(horarioUm) <= Convert.ToDateTime(horario.HoraSaida) && Convert.ToDateTime(horario.HoraSaida) <= Convert.ToDateTime(horarioDois))
                        {
                            lstPorHorarios.Add(linha);
                        }

                    }
                }
                linhas = lstPorHorarios.AsQueryable().OrderBy(x => x.Nome).Distinct();
            }
            return linhas.Select(x => Linha.SelectAdm(x)).ToList();
        }

        public Object GetLinha(int idLinha)
        {
            var query = Linha.SelectAdmDetails(_session.Query<Linha>().Where(x => x.Id == idLinha).ToList().FirstOrDefault());
            return query;
        }

        public IList<Linha> LinhasPorTipo(int idTipo)
        {
            IList<Linha> linhas = _session.Query<Linha>().Where(x => x.Tipo.Id == idTipo).ToList<Linha>();

            return linhas;
        }

        public IList<Linha> LinhasPorEmpresa(int idEmpresa)
        {
            IList<Linha> linhas = _session.Query<Linha>().Where(x => x.Empresa.Id == idEmpresa).ToList<Linha>();

            return linhas;

        }
    }
}