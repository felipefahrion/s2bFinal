using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace DesgrudaCoisa.Models
{
    //public class InicializadorBancoDeDados : System.Data.Entity.DropCreateDatabaseIfModelChanges<MapeamentoEntidadesContext>
    public class InicializadorBancoDeDados : System.Data.Entity.DropCreateDatabaseAlways<MapeamentoEntidadesContext>
    {
        protected override void Seed(MapeamentoEntidadesContext context)
        {

            var categorias = new List<Categoria>
            {
                new Categoria { TituloCategoria = "Informatica",  Descricao = "Produtos Informatica"},
                new Categoria { TituloCategoria = "Estetica",  Descricao = "Produtos de Beleza"},

             };
            categorias.ForEach(s => context.Categorias.Add(s));
            context.SaveChanges();


            var anuncios = new List<Anuncio>
            {
                new Anuncio {
                    TituloAnuncio = "Mouse",
                    Valor = 10,
                    DataPublicacao = DateTime.Parse("1/11/1989",new CultureInfo("en-US")),
                    CategoriaID =  categorias.Single( g => g.TituloCategoria == "Informatica").IdCategoria,
                },
                new Anuncio {
                    TituloAnuncio = "Shampo anti careca",
                    Valor = 20,
                    DataPublicacao = DateTime.Parse("1/11/2002",new CultureInfo("en-US")),
                    CategoriaID =  categorias.Single( g => g.TituloCategoria == "Estetica").IdCategoria,
                },
             };
            anuncios.ForEach(s => context.Anuncios.Add(s));
            context.SaveChanges();
        }
    }
}