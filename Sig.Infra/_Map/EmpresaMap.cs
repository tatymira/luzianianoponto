using FluentNHibernate.Mapping;
using Sig.Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sig.Infra._Map
{
    public class EmpresaMap : ClassMap<Empresa>

    {
        public EmpresaMap()
        {

            Id(x => x.Id).Column("idempresa").GeneratedBy.Identity();
            Map(x => x.Cnpj); 
            Map(x => x.RazaoSocial); 
            Map(x => x.Nome); 
            Map(x => x.Email);
            Map(x => x.Senha);
            Map(x => x.Perfil);

            Table("empresa");
        }

    }
}