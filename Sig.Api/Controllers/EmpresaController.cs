using Newtonsoft.Json;
using Sig.Domain.Classes;
using Sig.Domain.Enum;
using Sig.Domain.IRepositories;
using Sig.Infra;
using Sig.Infra.__Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Sig.Api.Controllers
{
    public class EmpresaController : ApiController
    {
        private readonly ILinhaRepository _linhaRep;
        private readonly IEmpresaRepository _empresaRep;
        private readonly IUnitOfWork _uow;

        public EmpresaController(ILinhaRepository linhaRep,
            IEmpresaRepository empresaRep,
            IUnitOfWork uow
)
        {
            _linhaRep = linhaRep;
            _empresaRep = empresaRep;
            _uow = uow;
        }

        [HttpPost]
        [Route("empresa")]
        public IHttpActionResult CadastrarEmpresa([FromBody] dynamic jsonPostData)
        {
            try
            {
                Empresa empresa = JsonConvert.DeserializeObject<Empresa>(jsonPostData.empresa.ToString()) as Empresa;

                if (empresa.Id == 0)
                {
                    var host = HttpContext.Current.Request.UrlReferrer.AbsoluteUri;
                    string link = host + "#/admEmpresas/" + empresa.Cnpj;
                    var lstParametros = new List<KeyValuePair<string, string>>();
                    lstParametros.Add(new KeyValuePair<string, string>("@cnpj", empresa.Cnpj));
                    lstParametros.Add(new KeyValuePair<string, string>("@nome", empresa.Nome));
                    lstParametros.Add(new KeyValuePair<string, string>("@razaoSocial", empresa.RazaoSocial));
                    lstParametros.Add(new KeyValuePair<string, string>("@link", link));

                    EmailApp.RegistrarSenha(empresa.Email, lstParametros);

                    empresa.Perfil = PerfilEnum.Administrador;
                    empresa.Senha = null;
                }

                _uow.BeginTransaction();
                _empresaRep.SaveOrUpdate(empresa);
                _uow.Execute();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("empresa/listar")]
        public IHttpActionResult ListarEmpresas()
        {
            try
            {
                var list = _empresaRep.ListarEmpresas();

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("empresa")]
        public IHttpActionResult EditarEmpresa(int id)
        {
            try
            {
                var empresa = _empresaRep.Find(id);
                return Ok(empresa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("empresa/excluir")]
        public IHttpActionResult ExcluirEmpresa(int id)
        {
            try
            {

                var linhasPorTipo = _linhaRep.LinhasPorEmpresa(id).Count();
                if (linhasPorTipo > 0)
                {
                    return Ok("A empresa não pode ser excluída porque existem linhas associadas a ela.");
                }
                else
                {
                    _uow.BeginTransaction();
                    _empresaRep.Delete(id);
                    _uow.Execute();

                    return Ok();
                }

                var list = _empresaRep.ListarEmpresas();

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("empresa/cadastrar-senha")]
        public IHttpActionResult CadastrarSenha([FromBody] dynamic jsonPostData)
        {
            try
            {
                string senha1 = jsonPostData.senha1.ToString();
                string senha2 = jsonPostData.senha2.ToString();
                string cnpj = jsonPostData.cnpj.ToString();


                Empresa empresa = _empresaRep.GetEmpresaPorCnpj(cnpj);


                if (senha1 == senha2)
                {
                    if (empresa.Senha == null)
                    {
                        empresa.Senha = senha1;
                        _uow.BeginTransaction();
                        _empresaRep.SaveOrUpdate(empresa);
                        _uow.Execute();
                    }
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
