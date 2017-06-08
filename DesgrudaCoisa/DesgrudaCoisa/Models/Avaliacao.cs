using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DesgrudaCoisa.Models
{
    public class Avaliacao
    {
        [Key]
        public int AvaliacaoID { get; set; }
        public bool Avaliado { get; set; }
        public int numEstrela { get; set; }

        public int AnuncioID { get; set; }
        public virtual Anuncio Anuncio { get; set; }

    }
}