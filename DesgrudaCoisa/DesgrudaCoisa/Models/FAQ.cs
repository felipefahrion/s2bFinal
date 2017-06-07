using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DesgrudaCoisa.Models
{
    public class FAQ
    {
        [Key]
        public int FaqID { get; set; }
        public string UsuarioEmail { get; set; }
        public string Pergunta { get; set; }
        public string Resposta { get; set; }

        public int AnuncioID { get; set; }
        public Anuncio Anuncios { get; set; }
    }
}