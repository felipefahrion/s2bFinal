using System.Data.Entity;

namespace DesgrudaCoisa.Models
{
    public class MapeamentoEntidadesContext : DbContext
    {
        public MapeamentoEntidadesContext() : base("InicializadorBD") { }
        public DbSet<Anuncio> Anuncios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Imagem> Imagens { get; set; }
        public DbSet<StatusAnuncio> StatusAnuncio { get; set; }
        public DbSet<FAQ> Faqs { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }
    }
}