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
    public class ParadaController : ApiController
    {

        private readonly IParadaRepository _paradaRep;
        private readonly IUnitOfWork _uow;

        public ParadaController(IParadaRepository paradaRep,
        IUnitOfWork uow)
        {
            _paradaRep = paradaRep;
            _uow = uow;
        }

        [HttpGet]
        [Route("parada/renderizar")]
        public IHttpActionResult RenderizarParadas()
        {
            return Ok(_paradaRep.ListarTodasParadas());
        }

        [HttpPost]
        [Route("parada")]
        public IHttpActionResult CadastrarParada([FromBody] dynamic jsonPostData)
        {
            try
            {
                IList<Parada> paradasParaInserir = JsonConvert.DeserializeObject<IList<Parada>>(jsonPostData.lstparadas.ToString()) as IList<Parada>;
                IList<Parada> paradasJaPersistidas = _paradaRep.ListarTodasParadas();
                
                foreach(Parada parada in paradasJaPersistidas)
                {
                    _uow.BeginTransaction();
                    _paradaRep.Delete(parada);
                    _uow.Execute();
                }

                foreach (Parada parada in paradasParaInserir)
                {
                    _uow.BeginTransaction();
                    _paradaRep.Save(parada);
                    _uow.Execute();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
