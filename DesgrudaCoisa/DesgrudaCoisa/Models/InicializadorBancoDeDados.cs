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

            var imagens = new List<Imagem>
            {
                new Imagem {ImageUrl = "/Images/mouse.jpg"},
                new Imagem {ImageUrl = "/Images/livro.jpg"},
                new Imagem {ImageUrl = "/Images/halteres.jpg"},
                new Imagem {ImageUrl = "/Images/crememassagem.jpg"},
             };

            imagens.ForEach(s => context.Imagens.Add(s));
            context.SaveChanges();

            var statusAnuncio = new List<StatusAnuncio>
            {
                new StatusAnuncio {Descricao = "Vendido"},
                new StatusAnuncio {Descricao= "Em negociacao"},
                new StatusAnuncio {Descricao= "Desistencia de venda"},
                new StatusAnuncio {Descricao= "Disponivel"},
                new StatusAnuncio {Descricao= "Doado"},
                new StatusAnuncio {Descricao= "Bloqueado"},

             };

            statusAnuncio.ForEach(s => context.StatusAnuncio.Add(s));
            context.SaveChanges();

            var anuncios = new List<Anuncio>
            {
                new Anuncio {
                    TituloAnuncio = "Mouse",
                    Valor = 10,
                    Descricao = "Mouse wireless de Computador com entrada USB 2.0",
                    StatusID = statusAnuncio.Single( g => g.Descricao == "Disponivel").StatusID,
                    ImagemID = imagens.Single( g => g.ImageUrl == "/Images/mouse.jpg").ImagemID,
                    CategoriaID =  categorias.Single( g => g.TituloCategoria == "Informatica").CategoriaID,
                    DataPublicacao = DateTime.Parse("1986-2-23"),
                    VendedorEmail = "admin@mvc.br",
                },
                new Anuncio {
                    TituloAnuncio = "Livro Steve Jobs",
                    Valor = 20,
                    Descricao = "A Cabeça de Steve Jobs é um livro de Marcio Edson Tavares, lançado em 2010, que retrata aspectos da vida e personalidade de Steve Jobs. Conforme sinopse da Livraria Cultura, o livro é, ao mesmo tempo, uma biografia e um guia sobre liderança.",
                    StatusID = statusAnuncio.Single( g => g.Descricao == "Disponivel").StatusID,
                    ImagemID = imagens.Single( g => g.ImageUrl == "/Images/livro.jpg").ImagemID,
                    CategoriaID =  categorias.Single( g => g.TituloCategoria == "Informatica").CategoriaID,
                    DataPublicacao = DateTime.Parse("1986-2-23"),
                    VendedorEmail = "admin@mvc.br",
                },
                new Anuncio {
                    TituloAnuncio = "Creme de massagem drenante",
                    Valor = 30,
                    Descricao = "Creme de massagem desenvolvido para auxiliar na ativação do sistema linfático, este creme com óleo de pimenta negra, termogênico de origem natural, é indicado para massagens modeladoras.",
                    StatusID = statusAnuncio.Single( g => g.Descricao == "Disponivel").StatusID,
                    ImagemID = imagens.Single( g => g.ImageUrl == "/Images/crememassagem.jpg").ImagemID,
                    CategoriaID =  categorias.Single( g => g.TituloCategoria == "Estetica").CategoriaID,
                    DataPublicacao = DateTime.Parse("1986-2-23"),
                    VendedorEmail = "admin@mvc.br",
                },
                new Anuncio {
                    TituloAnuncio = "Halteres 20 kg",
                    Valor = 40,
                    Descricao = "2 (dois) Halteres, bem cuidados da marca Phonex. Com um ano de garantia",
                    StatusID = statusAnuncio.Single( g => g.Descricao == "Disponivel").StatusID,
                    ImagemID = imagens.Single( g => g.ImageUrl == "/Images/halteres.jpg").ImagemID,
                    CategoriaID =  categorias.Single( g => g.TituloCategoria == "Academia").CategoriaID,
                    DataPublicacao = DateTime.Parse("1986-2-23"),
                    VendedorEmail = "admin@mvc.br",
                },
             };
            anuncios.ForEach(s => context.Anuncios.Add(s));
            context.SaveChanges();

            var faqs = new List<FAQ>
            {
                new FAQ {Pergunta = "Qual Preço?", AnuncioID = anuncios.Single( g => g.TituloAnuncio == "Mouse").AnuncioID },
                new FAQ {Pergunta = "Está com garantia?", AnuncioID = anuncios.Single( g => g.TituloAnuncio == "Mouse").AnuncioID },

             };

            faqs.ForEach(s => context.Faqs.Add(s));
            context.SaveChanges();
        }
    }
}