using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Desgruda.Models
{
    public class Categoria
    {
        public int CategoriaID { get; set; }
        public string TituloCategoria { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Anuncio> Anuncios { get; set; }
    }
}