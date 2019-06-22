using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sig.Domain.Classes
{
    public class Horario
    {
        public virtual int Id { get; set; }
        public virtual string HoraSaida { get; set; }
        public virtual Linha Linha { get; set; }

        public static Object Select(Horario horario)
        {
            return new
            {
                Id = horario.Id,
                HoraSaida = horario.HoraSaida
            };
        }
    }
}