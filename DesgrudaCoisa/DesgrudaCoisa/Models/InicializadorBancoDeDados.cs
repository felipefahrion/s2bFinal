using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace DesgrudaCoisa.Models
{
    public class InicializadorBancoDeDados : System.Data.Entity.DropCreateDatabaseIfModelChanges<MapeamentoEntidadesContext>
    //public class InicializadorBancoDeDados : System.Data.Entity.DropCreateDatabaseAlways<MapeamentoEntidadesContext>
    {
        protected override void Seed(MapeamentoEntidadesContext context)
        {

            var categorias = new List<Categoria>
            {
                new Categoria { TituloCategoria = "Informatica",  Descricao = "Produtos Informatica"},
                new Categoria { TituloCategoria = "Estetica",  Descricao = "Produtos de Beleza"},
                new Categoria {TituloCategoria = "Academia", Descricao = "Produtos para Academia"},
                new Categoria {TituloCategoria = "Moda Masculina", Descricao = "Produtos para Moda Masculina"},
                new Categoria {TituloCategoria = "Moda Feminina", Descricao = "Produtos para Moda Feminina"},
                new Categoria {TituloCategoria = "Casa", Descricao = "Produtos para Casa"},
                new Categoria {TituloCategoria = "Automóveis", Descricao = "Produtos para Automóvel"},
                new Categoria {TituloCategoria = "Games", Descricao = "Produtos para Consoles"},
                new Categoria {TituloCategoria = "Acessórios", Descricao = "Acessórios para o dia a dia"},
                new Categoria {TituloCategoria = "Maquiagem", Descricao = "Produtos de Beleza"},
                new Categoria {TituloCategoria = "Livros", Descricao = "Produtos para leitura"},
                new Categoria {TituloCategoria = "Filmes e Séries", Descricao = "Entreterimento"},
                new Categoria {TituloCategoria = "Infantil", Descricao = "Produtos para crianças"},
                



             };
            categorias.ForEach(s => context.Categorias.Add(s));
            context.SaveChanges();

            var imagens = new List<Imagem>
            {
                new Imagem {ImageUrl = "/Images/mouse.jpg"},
                new Imagem {ImageUrl = "/Images/livro.jpg"},
                new Imagem {ImageUrl = "/Images/halteres.jpg"},
                new Imagem {ImageUrl = "/Images/crememassagem.jpg"},
                new Imagem {ImageUrl = "/Images/relogio.jpg"},
                new Imagem {ImageUrl = "/Images/packBreakingBad.jpg"},
                new Imagem {ImageUrl = "/Images/banquetas.jpg"},
                new Imagem {ImageUrl = "/Images/carrinhoBebe.jpg"},

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
                    TituloAnuncio = "Carrinho de bebê",
                    Valor = 474,
                    Descricao = "Carrinho de passeio para bebê da marca Galzerano",
                    StatusID = statusAnuncio.Single( g => g.Descricao == "Disponivel").StatusID,
                    ImagemID = imagens.Single( g => g.ImageUrl == "/Images/carrinhoBebe.jpg").ImagemID,
                    CategoriaID =  categorias.Single( g => g.TituloCategoria == "Infantil").CategoriaID,
                    DataPublicacao = DateTime.Parse("1986-2-23"),
                    VendedorEmail = "admin@s2b.br",
                    Local = "Porto Alegre"
                },
                new Anuncio {
                    TituloAnuncio = "Banquetas",
                    Valor = 150,
                    Descricao = "Banquetas para uso interno da casa. Idela em lugares com lobbys compartilhados.",
                    StatusID = statusAnuncio.Single( g => g.Descricao == "Disponivel").StatusID,
                    ImagemID = imagens.Single( g => g.ImageUrl == "/Images/banquetas.jpg").ImagemID,
                    CategoriaID =  categorias.Single( g => g.TituloCategoria == "Casa").CategoriaID,
                    DataPublicacao = DateTime.Parse("1986-2-23"),
                    VendedorEmail = "admin@s2b.br",
                    Local = "Paraná"
                },

                new Anuncio {
                    TituloAnuncio = "Box Braking Bad",
                    Valor = 251,
                    Descricao = "Box com todas as temporadas de Breaking Bad, em Blu-Ray. O ator protagosnira ganhou o Emmy Awards em 2009.",
                    StatusID = statusAnuncio.Single( g => g.Descricao == "Disponivel").StatusID,
                    ImagemID = imagens.Single( g => g.ImageUrl == "/Images/packBreakingBad.jpg").ImagemID,
                    CategoriaID =  categorias.Single( g => g.TituloCategoria == "Filmes e Séries").CategoriaID,
                    DataPublicacao = DateTime.Parse("1986-2-23"),
                    VendedorEmail = "admin@s2b.br",
                    Local = "Santa Catarina"
                },
                new Anuncio {
                    TituloAnuncio = "Rélógio",
                    Valor = 1200,
                    Descricao = "Relógio da marca Invicta, apenas 2 meses de uso.",
                    StatusID = statusAnuncio.Single( g => g.Descricao == "Disponivel").StatusID,
                    ImagemID = imagens.Single( g => g.ImageUrl == "/Images/relogio.jpg").ImagemID,
                    CategoriaID =  categorias.Single( g => g.TituloCategoria == "Acessórios").CategoriaID,
                    DataPublicacao = DateTime.Parse("1986-2-23"),
                    VendedorEmail = "admin@s2b.br",
                    Local = "Acre"
                },

                new Anuncio {
                    TituloAnuncio = "Mouse",
                    Valor = 80,
                    Descricao = "Mouse wireless de Computador com entrada USB 2.0",
                    StatusID = statusAnuncio.Single( g => g.Descricao == "Disponivel").StatusID,
                    ImagemID = imagens.Single( g => g.ImageUrl == "/Images/mouse.jpg").ImagemID,
                    CategoriaID =  categorias.Single( g => g.TituloCategoria == "Informatica").CategoriaID,
                    DataPublicacao = DateTime.Parse("1986-2-23"),
                    VendedorEmail = "admin@s2b.br",
                    Local = "Rio de Janeiro"
                },
                new Anuncio {
                    TituloAnuncio = "Livro Steve Jobs",
                    Valor = 23,
                    Descricao = "A Cabeça de Steve Jobs é um livro de Marcio Edson Tavares, lançado em 2010, que retrata aspectos da vida e personalidade de Steve Jobs. Conforme sinopse da Livraria Cultura, o livro é, ao mesmo tempo, uma biografia e um guia sobre liderança.",
                    StatusID = statusAnuncio.Single( g => g.Descricao == "Disponivel").StatusID,
                    ImagemID = imagens.Single( g => g.ImageUrl == "/Images/livro.jpg").ImagemID,
                    CategoriaID =  categorias.Single( g => g.TituloCategoria == "Informatica").CategoriaID,
                    DataPublicacao = DateTime.Parse("1986-2-23"),
                    VendedorEmail = "admin@s2b.br",
                    Local = "Caxias do Sul"
                },
                new Anuncio {
                    TituloAnuncio = "Creme de massagem drenante",
                    Valor = 103,
                    Descricao = "Creme de massagem desenvolvido para auxiliar na ativação do sistema linfático, este creme com óleo de pimenta negra, termogênico de origem natural, é indicado para massagens modeladoras.",
                    StatusID = statusAnuncio.Single( g => g.Descricao == "Disponivel").StatusID,
                    ImagemID = imagens.Single( g => g.ImageUrl == "/Images/crememassagem.jpg").ImagemID,
                    CategoriaID =  categorias.Single( g => g.TituloCategoria == "Estetica").CategoriaID,
                    DataPublicacao = DateTime.Parse("1986-2-23"),
                    VendedorEmail = "admin@s2b.br",
                    Local = "Recife"
                },
                new Anuncio {
                    TituloAnuncio = "Halteres 20 kg",
                    Valor = 276,
                    Descricao = "2 (dois) Halteres, bem cuidados da marca Phonex. Com um ano de garantia",
                    StatusID = statusAnuncio.Single( g => g.Descricao == "Disponivel").StatusID,
                    ImagemID = imagens.Single( g => g.ImageUrl == "/Images/halteres.jpg").ImagemID,
                    CategoriaID =  categorias.Single( g => g.TituloCategoria == "Academia").CategoriaID,
                    DataPublicacao = DateTime.Parse("1986-2-23"),
                    VendedorEmail = "admin@s2b.br",
                    Local = "São Paulo"
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