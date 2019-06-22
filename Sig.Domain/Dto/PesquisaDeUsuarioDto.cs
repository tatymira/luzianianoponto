using Sig.Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sig.Domain.Dto
{
    public class PesquisaDeUsuarioDto
    {

        public string Trajeto { get; set; }

        public int Linha { get; set; }

        public string HorarioUm { get; set; }

        public string HorarioDois { get; set; }
    }
}