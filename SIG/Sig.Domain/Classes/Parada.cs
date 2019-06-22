using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace Sig.Domain.Classes
{
    public class Parada
    {
        public virtual int Id { get; set; }

        public virtual int Numero { get; set; }

        public virtual double Longitude { get; set; }

        public virtual double Latitude { get; set; }
        

    }
}