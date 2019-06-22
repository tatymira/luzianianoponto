using Sig.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sig.Domain.Classes
{
    public class Sugestao
    {

        public virtual int Id { get; set; }

        public virtual double Longitude { get; set; }

        public virtual double Latitude { get; set; }

        public virtual string Descricao { get; set; }

        public virtual PerfilEnum Alocacao { get; set; }

        public virtual Linha Linha { get; set; }

        public virtual bool Ativo { get; set; }

        public virtual DateTime Data { get; set; }


        public static Object Select(Sugestao sugestao)
        {
            return new
            {
                Id = sugestao.Id,
                Descricao = sugestao.Descricao,
                Alocacao = sugestao.Alocacao,
                Numero = sugestao.Linha.Numero,
                Empresa = sugestao.Linha.Empresa.Nome,
                Data = sugestao.Data,
                Linha = sugestao.Linha.Nome

            };
        }

    }
}