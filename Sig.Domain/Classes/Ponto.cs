using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sig.Domain.Classes
{
    public class Ponto
    {
        public virtual double Lng { get; set; }

        public virtual double Lat { get; set; }

        public Ponto()
        {

        }
        public Ponto(double Lat, double Lng)
        {
            this.Lat = Lat;
            this.Lng = Lng;
        }
    }
}