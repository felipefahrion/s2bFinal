using System.Data.Entity;

namespace DesgrudaCoisa.Models
{
    public class MapeamentoEntidadesContext : DbContext
    {
        public MapeamentoEntidadesContext() : base("InicializadorBD") { }
        public DbSet<Anuncio> Anuncios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}