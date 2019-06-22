using Sig.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sig.Domain.Classes
{
    public class Empresa
    {
        public virtual int Id { get; set; }

        public virtual string Cnpj { get; set; }

        public virtual string RazaoSocial { get; set; }

        public virtual string Nome { get; set; }

        public virtual string Email { get; set; }

        public virtual string Senha { get; set; }

        public virtual PerfilEnum Perfil { get; set; }
    }
}