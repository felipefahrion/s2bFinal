using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesgrudaCoisa.Models
{
    public class FAQ
    {
        public int FaqID { get; set; }
        public string UsuarioEmail { get; set; }
        public string Pergunta { get; set; }
        public string Resposta { get; set; }

        public IEnumerable<AnuncioFAQ> AnunciosFAQ { get; set; }
    }
}