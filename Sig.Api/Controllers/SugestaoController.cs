using Sig.Domain.Classes;
using Sig.Domain.Enum;
using Sig.Domain.IRepositories;
using Sig.Infra.__Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sig.Api.Controllers
{
    public class SugestaoController : ApiController
    {
        private readonly IUnitOfWork _uow;
        private readonly ISugestaoRepository _sugestaoRep;
        private readonly ILinhaRepository _linhaRep;

        public SugestaoController(IUnitOfWork uow,
            ISugestaoRepository sugestaoRep,
            ILinhaRepository linhaRep)
        {
            _uow = uow;
            _linhaRep = linhaRep;
            _sugestaoRep = sugestaoRep;
        }

        [HttpPost]
        [Route("sugestao")]
        public IHttpActionResult EnviarSugestao([FromBody] dynamic jsonPostData)
        {
            try
            {
                string texto = jsonPostData.sugestao;
                int empresa = jsonPostData.empresaId;
                int idLinha = jsonPostData.linhaId;

                Sugestao sugestao = new Sugestao();
                sugestao.Alocacao = PerfilEnum.Administrador;
                sugestao.Descricao = texto;
                sugestao.Ativo = true;
                sugestao.Data = DateTime.Now;
                sugestao.Linha = _linhaRep.Find(idLinha);

                _uow.BeginTransaction();
                _sugestaoRep.SaveOrUpdate(sugestao);
                _uow.Execute();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("sugestoes/listar")]
        public IHttpActionResult ListarSugestoes(string nomeEmpresa)
        {
            return Ok(_sugestaoRep.ListarSugestoes(nomeEmpresa));
        }

        [HttpPost]
        [Route("sugestao/lancar")]
        public IHttpActionResult LancarSugestao([FromBody] dynamic jsonPostData)
        {
            int id = jsonPostData.idModal;
            Sugestao sugestao = _sugestaoRep.Find(id);
            if (sugestao.Alocacao == PerfilEnum.Master)
            {
                sugestao.Alocacao = PerfilEnum.Administrador;
            }
            else
            {
                sugestao.Alocacao = PerfilEnum.Master;
            }

            _uow.BeginTransaction();
            _sugestaoRep.SaveOrUpdate(sugestao);
            _uow.Execute();

            return Ok();
        }

        [HttpPost]
        [Route("sugestao/finalizar")]
        public IHttpActionResult FinalizarSugestao([FromBody] dynamic jsonPostData)
        {
            int id = jsonPostData.idSugestao;
            Sugestao sugestao = _sugestaoRep.Find(id);
            sugestao.Ativo = false;

            _uow.BeginTransaction();
            _sugestaoRep.SaveOrUpdate(sugestao);
            _uow.Execute();

            return Ok();
        }
    }
}
