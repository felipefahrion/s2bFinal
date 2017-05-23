using System.Data.Entity;

namespace Desgruda.Models
{
    public class MapeamentoEntidadesContext : DbContext
    {
        public MapeamentoEntidadesContext() : base("InicializadorBD") { }
        public DbSet<Anuncio> Anuncios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}