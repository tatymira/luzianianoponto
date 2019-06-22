using Newtonsoft.Json;
using Sig.Domain.Classes;
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
    public class TipoController : ApiController
    {

        private readonly ITipoRepository _tipoRep;
        private readonly ILinhaRepository _linhaRep;
        private readonly IUnitOfWork _uow;

        public TipoController(ITipoRepository tipoRep, ILinhaRepository linhaRep, IUnitOfWork uow)
        {
            _tipoRep = tipoRep;
            _linhaRep = linhaRep;
            _uow = uow;
        }
        

        [HttpPost]
        [Route("tipo")]
        public IHttpActionResult CadastrarTipo([FromBody] dynamic jsonPostData)
        {
            try
            {
                Tipo tipo = JsonConvert.DeserializeObject<Tipo>(jsonPostData.tipo.ToString()) as Tipo;

                _uow.BeginTransaction();
                _tipoRep.SaveOrUpdate(tipo);
                _uow.Execute();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("tipo")]
        public IHttpActionResult ListarTipos()
        {
            try
            {
                var list = _tipoRep.ListarTipos();

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("tipo/editar")]
        public IHttpActionResult EditarTipo(int id)
        {
            try
            {
                return Ok(_tipoRep.Find(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("tipo/excluir")]
        public IHttpActionResult ExcluirTipo(int id)
        {
            try
            {
                var linhasPorTipo = _linhaRep.LinhasPorTipo(id).Count();
                if(linhasPorTipo > 0)
                {
                    return Ok("O tipo de linha não pode ser excluído porque existem linhas associadas a ele.");
                }else
                {
                    _uow.BeginTransaction();
                    _tipoRep.Delete(id);
                    _uow.Execute();

                    return Ok();
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
