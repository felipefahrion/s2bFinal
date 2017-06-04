using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DesgrudaCoisa.Models
{
    public class Anuncio
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Título do Anúncio")]
        public string TituloAnuncio { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Display(Name = "Preço")]
        public decimal Valor { get; set; }
        [Required]
        public string VendedorEmail { get; set; }
        public string CompradorEmail { get; set; }

        public int CategoriaID { get; set; }
        public virtual Categoria Categoria { get; set; }

        public int ImagemID { get; set; }
        public virtual Imagem Imagem { get; set; }

        public int EnumStatusID { get; set; }
        public virtual EnumStatus EnumStatus { get; set; }
        
        public int? FaqID { get; set; }
        public IEnumerable<FAQ> Faq { get; set; }


    }
}