using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesgrudaCoisa.Models
{
    public class Anuncio
    {

        public int ID { get; set; }
        public string TituloAnuncio { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataPublicacao { get; set; }

        public int CategoriaID { get; set; }
        public virtual Categoria Categoria { get; set; }

        public virtual Imagem Imagem { get; set; }

        public virtual EnumStatus EnumStatus { get; set; }

        public IEnumerable<AnuncioFAQ> AnunciosFAQ { get; set; }


    }
}