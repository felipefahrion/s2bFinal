using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DesgrudaCoisa.Models
{
    public class Categoria
    {
        [Display(Name = "Categoria")]
        public int CategoriaID { get; set; }
        [Display(Name = "Título")]
        public string TituloCategoria { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public virtual ICollection<Anuncio> Anuncios { get; set; }
    }
}