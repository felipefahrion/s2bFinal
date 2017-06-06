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
        public int AnuncioID { get; set; }

        [Display(Name = "Título do Anúncio")]
        [StringLength(60, MinimumLength = 3)]
        public string TituloAnuncio { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Display(Name = "Preço")]
        public decimal Valor { get; set; }
        [Required]
        public string VendedorEmail { get; set; }
        public string CompradorEmail { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        [Display(Name = "Data de Publicação")]
        public DateTime DataPublicacao { get; set; }

        public int CategoriaID { get; set; }
        public virtual Categoria Categoria { get; set; }

        public int ImagemID { get; set; }
        public virtual Imagem Imagem { get; set; }

        public int StatusID { get; set; }
        public virtual StatusAnuncio Status { get; set; }
        
        public ICollection<FAQ> Faq { get; set; }


    }
}