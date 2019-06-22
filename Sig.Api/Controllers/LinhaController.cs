using System;
using System.Web.Http;
using Sig.Domain.Classes;
using Newtonsoft.Json;
using Sig.Domain.IRepositories;
using System.Collections.Generic;
using System.Web.Http.Cors;
using Sig.Infra.__Base;
using Sig.Domain.Dto;
using System.Linq;

namespace Sig.Api.Controllers
{
    public class LinhaController : ApiController
    {
        private readonly ILinhaRepository _linhaRep;
        private readonly IHorarioRepository _horarioRep;
        private readonly IEmpresaRepository _empresaRep;
        private readonly ISugestaoRepository _sugestaoRep;
        private readonly IUnitOfWork _uow;

        public LinhaController(ILinhaRepository linhaRep,
            IHorarioRepository horarioRep,
            IEmpresaRepository empresaRep,
            ISugestaoRepository sugestaoRep,
        IUnitOfWork uow)
        {
            _linhaRep = linhaRep;
            _horarioRep = horarioRep;
            _empresaRep = empresaRep;
            _sugestaoRep = sugestaoRep;
            _uow = uow;
        }

        [HttpGet]
        [Route("linha/listar-empresa")]
        public IHttpActionResult ListarLinhasEmpresa(string nomeEmpresa)
        {
            return Ok(_linhaRep.ListarLinhasEmpresa(nomeEmpresa));
        }


        [HttpGet]
        [Route("linha/pesquisar")]
        public IHttpActionResult ListarLinhas(string filtro)
        {
            return Ok(_linhaRep.PesquisarLinhas(filtro));
        }


        [HttpPost]
        [Route("linha")]
        public IHttpActionResult CadastrarLinha([FromBody] dynamic jsonPostData)
        {
            try
            {
                Linha linha = JsonConvert.DeserializeObject<Linha>(jsonPostData.dadosLinha.ToString()) as Linha;
                var linhaTrajeto = jsonPostData.linhaTrajeto;
                var lstHorarios = jsonPostData.lstHorarios;
                var nomeEmpresa = jsonPostData.nomeEmpresa.ToString();
                
                linha.Horarios = new List<Horario>();
                foreach (var item in lstHorarios)
                {
                    Horario horario = JsonConvert.DeserializeObject<Horario>(item.ToString()) as Horario;
                    horario.Linha = linha;
                    linha.Horarios.Add(horario);
                };

                linha.Horarios = linha.Horarios.OrderBy(o => o.HoraSaida).ToList();

                IList<string> lstString = new List<string>();
                foreach (var item in linhaTrajeto)
                {
                    IList<Double> array = new List<Double>();
                    Ponto ponto = JsonConvert.DeserializeObject<Ponto>(item.ToString());
                    array.Add(ponto.Lat);
                    array.Add(ponto.Lng);
                    string trajeto = string.Join(" ", array).Replace(",", ".");
                    lstString.Add(trajeto);
                    trajeto = null;
                };

                string trajetoFinal = string.Join(",", lstString);

                linha.Origem = "Terminal";
                linha.Destino = "Terminal";
                linha.Empresa = _empresaRep.GetEmpresaPorRazaoSocial(nomeEmpresa);
                linha.LinhaMap = trajetoFinal;

                //if (linha.Id != 0)
                //{
                //    _horarioRep.ExcluirHorariosPorLinha(linha.Id);
                //}

                _uow.BeginTransaction();
                _linhaRep.SaveOrUpdate(linha);
                _uow.Execute();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("linha")]
        public IHttpActionResult GetLinha(int id)
        {
            return Ok(_linhaRep.GetLinha(id));
        }

        [HttpPost]
        [Route("linha/excluir")]
        public IHttpActionResult ExcluirLinha([FromBody] dynamic jsonPostData)
        {
            int id = jsonPostData.id;
            var lst = _sugestaoRep.ListarSugestoesPorLinha(id);

            _uow.BeginTransaction();
            foreach (Sugestao item in lst)
            {
                _sugestaoRep.Delete(item.Id);
            }
            _linhaRep.Delete(id);
            _uow.Execute();
            return Ok();
        }
    }
}
