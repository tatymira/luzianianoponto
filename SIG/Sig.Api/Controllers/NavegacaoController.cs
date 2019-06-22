using Sig.Domain.Classes;
using Sig.Domain.Enum;
using Sig.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sig.Api.Controllers
{
    public class NavegacaoController : ApiController
    {
        private readonly IEmpresaRepository _empRep;

        public NavegacaoController(
            IEmpresaRepository empRep)
        {
            _empRep = empRep;
        }


        [HttpGet]
        [Route("navegacao/logar")]
        public IHttpActionResult ValidarLogin(string nome, string senha)
        {           
            return Ok(_empRep.ValidarLogin(nome, senha));
        }
    }
}
