using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesgrudaCoisa.Models
{
    public class EnumStatus
    {
        public int EnumStatusID { get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<Anuncio> Anuncios { get; set; }

    }
}