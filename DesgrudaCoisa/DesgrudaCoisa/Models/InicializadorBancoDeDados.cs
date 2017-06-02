using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
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
                new Categoria {TituloCategoria = "Academia", Descricao = "Produtos para Academia"},

             };
            categorias.ForEach(s => context.Categorias.Add(s));
            context.SaveChanges();


            string byteString = "0x0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            byte[] byteArray = Encoding.ASCII.GetBytes(byteString);

            var imagens = new List<Imagem>
            {
                new Imagem {ImageFile = byteArray, ImageMimeType = "image/jpeg"},
             };

            imagens.ForEach(s => context.Imagens.Add(s));
            context.SaveChanges();

            var enumStatus = new List<EnumStatus>
            {
                new EnumStatus {Descricao = "Vendido"},
                new EnumStatus {Descricao= "Em negociacao"},

             };

            enumStatus.ForEach(s => context.EnumStatus.Add(s));
            context.SaveChanges();

            var anuncios = new List<Anuncio>
            {
                new Anuncio {
                    TituloAnuncio = "Mouse",
                    Valor = 10,
                    Descricao = "Mouse de Computador",
                    EnumStatusID = enumStatus.Single( g => g.Descricao == "Vendido").EnumStatusID,
                    ImagemID = 1,
                    CategoriaID =  categorias.Single( g => g.TituloCategoria == "Informatica").CategoriaID,
                    VendedorEmail = "admin@mvc.br",
                },
                //new Anuncio {
                //    TituloAnuncio = "Shampo anti careca",
                //    Valor = 20,
                //    CategoriaID =  categorias.Single( g => g.TituloCategoria == "Estetica").CategoriaID,
                //},
                //new Anuncio {
                //    TituloAnuncio = "Shaker",
                //    Valor = 50,
                //    CategoriaID =  categorias.Single( g => g.TituloCategoria == "Academia").CategoriaID,
                //},
             };
            anuncios.ForEach(s => context.Anuncios.Add(s));
            context.SaveChanges();
        }
    }
}