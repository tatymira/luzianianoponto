
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sig.Domain.Classes
{
    public class Linha
    {
        public virtual int Id { get; set; }

        public virtual string Nome { get; set; }

        public virtual decimal Tarifa { get; set; }

        public virtual string Origem { get; set; }

        public virtual string Destino { get; set; }

        public virtual int Numero { get; set; }

        public virtual Tipo Tipo { get; set; }

        public virtual Empresa Empresa { get; set; }

        public virtual string LinhaMap { get; set; }

        public virtual IList<Horario> Horarios { get; set; }


        public static Object SelectAdm(Linha linha)
        {
            return new
            {
                Id = linha.Id,
                Nome = linha.Nome,
                Tarifa = linha.Tarifa,
                Numero = linha.Numero,
                Tipo = linha.Tipo.Descricao,
                LinhaMap = FormatarCoordenadasLinhas(linha.LinhaMap),
                Partidas = linha.Horarios.Count(),
                Empresa = linha.Empresa

            };
        }
        public static Object SelectAdmDetails(Linha linha)
        {
            return new
            {
                Id = linha.Id,
                Nome = linha.Nome,
                Tarifa = linha.Tarifa,
                Numero = linha.Numero,
                Tipo = linha.Tipo,
                Partidas = linha.Horarios.Count(),
                LinhaMap = FormatarCoordenadasLinhas(linha.LinhaMap),
                Empresa = linha.Empresa,
                Horarios = linha.Horarios.Select(x => Horario.Select(x)).ToArray()

            };
        }
        public static IList<Object> FormatarCoordenadasLinhas(string linhaMap)
        {
            IList<Object> resultado = new List<Object>();
            var array = linhaMap.Split(',');
            foreach (var a in array)
            {
                Ponto objLatLng = new Ponto();
                var array2 = a.Replace(".", ",").Split(' ');
                if (array2.Count() == 2)
                {
                    objLatLng.Lat = Convert.ToDouble(array2[0]);
                    objLatLng.Lng = Convert.ToDouble(array2[1]);
                }
                else
                {
                    objLatLng.Lat = Convert.ToDouble(array2[1]);
                    objLatLng.Lng = Convert.ToDouble(array2[2]);
                }

                resultado.Add(objLatLng);
            }
            return resultado;
        }
    }
}