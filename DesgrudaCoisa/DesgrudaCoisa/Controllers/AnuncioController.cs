using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DesgrudaCoisa.Models;
using Microsoft.AspNet.Identity;

namespace DesgrudaCoisa.Controllers
{
    public class AnuncioController : Controller
    {
        private MapeamentoEntidadesContext db = new MapeamentoEntidadesContext();

        // GET: Anuncio
        public ActionResult ListAnuncios(string pesquisar)
        {
            var anuncios = db.Anuncios.Include(a => a.Categoria);

            //pode ser assim tambem
            //var movies = from anuncio in movieDb.Anuncios select anuncio;

            if (!String.IsNullOrEmpty(pesquisar))
            {
                anuncios = anuncios.Where(s => s.TituloAnuncio.Contains(pesquisar));
            }
            return View(anuncios.ToList());
        }
        // GET: Anuncio/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Anuncio anuncio = db.Anuncios.Include(x=>x.Faq).Where(x=>x.AnuncioID == id).First();
            Anuncio anuncio = db.Anuncios.Find(id);
            if (anuncio == null)
            {
                return HttpNotFound();
            }
            IEnumerable<FAQ> listFaq = db.Faqs.Where(x => x.AnuncioID == anuncio.AnuncioID).ToList();
            if (listFaq.Count() >= 1)
            {
                ViewBag.faqs = listFaq;
            }
                        
            if (anuncio.numeroEstrelas >= 1)
            {
                ViewBag.numEstrela = anuncio.numeroEstrelas;
            }

            if (Request.IsAuthenticated)
            {
                string emailVend = User.Identity.GetUserName();
                if (anuncio.VendedorEmail == emailVend)
                {
                    ViewBag.VendedorLogado = true;
                }
            }
            return View(anuncio);
        }

        // GET: Anuncio/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "TituloCategoria");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(String tituloAnuncio, String descricao, decimal valor, int categoriaID, HttpPostedFileBase image)
        {
            Imagem imagem = new Imagem { ImageFile = new byte[image.ContentLength], ImageMimeType = image.ContentType };
            image.InputStream.Read(imagem.ImageFile, 0, image.ContentLength);
            db.Imagens.Add(imagem);
            db.SaveChanges();
            Anuncio anuncio = new Anuncio
            {
                TituloAnuncio = tituloAnuncio,
                Valor = valor,
                Descricao = descricao,
                StatusID = db.StatusAnuncio.Single(g => g.Descricao == "Disponivel").StatusID,
                ImagemID = imagem.ImagemID,
                CategoriaID = categoriaID,
                VendedorEmail = "admin@mvc.br",
                DataPublicacao = DateTime.Now
            };
            if (ModelState.IsValid)
            {
                db.Anuncios.Add(anuncio);
                db.SaveChanges();
                return RedirectToAction("ListAnuncios");
            }

            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "TituloCategoria", anuncio.CategoriaID);
            return View(anuncio);
        }
        // GET: Anuncio/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anuncio anuncio = db.Anuncios.Find(id);
            if (anuncio == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "TituloCategoria", anuncio.CategoriaID);
            return View(anuncio);
        }

        // POST: Anuncio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AnuncioID,TituloAnuncio,Valor,DataPublicacao,CategoriaID")] Anuncio anuncio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(anuncio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "TituloCategoria", anuncio.CategoriaID);
            return View(anuncio);
        }


        public ActionResult ListMeusAnuncios(string searchString)
        {
            var anuncios = db.Anuncios.Include(a => a.Categoria);

            //pode ser assim tambem
            //var movies = from anuncio in movieDb.Anuncios select anuncio;

            if (!String.IsNullOrEmpty(searchString))
            {
                anuncios = anuncios.Where(s => s.TituloAnuncio.Contains(searchString));
            }
            return View(anuncios.ToList());
        }

        public ActionResult GetImage(int id)
        {
            Anuncio anuncio = db.Anuncios.Find(id);
            if (anuncio != null && anuncio.Imagem.ImageFile != null)
            {
                return File(anuncio.Imagem.ImageFile, anuncio.Imagem.ImageMimeType);
            }
            else
            {
                return new FilePathResult("~/Images/1.jpg", "image/jpeg");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



        // AJAX: /Anuncio/ComprarAnuncio/5
        [HttpPost]
        [Authorize]
        public ActionResult ComprarAnuncio(int id)
        {
            var anuncio = db.Anuncios.Find(id);
            if (anuncio == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            string anuncioTitulo = anuncio.TituloAnuncio;
            var status = db.StatusAnuncio.Where(x => x.Descricao == "Em negociacao").First();
            if (Request.IsAuthenticated)
            {
                anuncio.CompradorEmail = User.Identity.GetUserName();
            }
            anuncio.StatusID = status.StatusID;

            db.Entry(anuncio).State = EntityState.Modified;
            db.SaveChanges();

            var results = new
            {
                Message = anuncioTitulo + " está em negociação.",
            };
            return Json(results);
        }

        public ActionResult BadgesNotificacoes()
        {
            string emailUsuarioLogado = null;
            if (Request.IsAuthenticated)
            {
                emailUsuarioLogado = User.Identity.GetUserName();
                int numAnunciosNegociacao = db.Anuncios.Where(x => (x.Status.Descricao == "Em negociacao") && (x.VendedorEmail == emailUsuarioLogado)).Count();
                int numAnunciosVendido = db.Anuncios.Where(x => (x.Status.Descricao == "Vendido") && (x.CompradorEmail == emailUsuarioLogado) && (x.Avaliado == false)).Count();
                ViewBag.NumAnunciosNegociacao = numAnunciosNegociacao + numAnunciosVendido;
            }
            return this.PartialView();
        }
        [Authorize]
        public ActionResult ListNotificacoesUsuario()
        {
            string emailUsuarioLogado = null;
            if (Request.IsAuthenticated)
            {
                emailUsuarioLogado = User.Identity.GetUserName();
                //vendedor deve verificar os pedidos de compra
                //ViewBag.Vendedor = db.Anuncios.Include(a => a.Status).Where(x => (x.Status.Descricao == "Em negociacao") && (x.VendedorEmail == emailUsuarioLogado));
                IEnumerable<Anuncio> anuncioListEmNegocio = db.Anuncios.Include(a => a.Status).Where(x => (x.Status.Descricao == "Em negociacao") && (x.VendedorEmail == emailUsuarioLogado));
                ViewBag.Vendedor = anuncioListEmNegocio;
                ViewBag.VendedorResult = anuncioListEmNegocio.ToList().Count();
                //comprador deve avaliar os seus produtos comprados
                IEnumerable<Anuncio> anuncioListVendido = db.Anuncios.Include(a => a.Status).Where(x => (x.Status.Descricao == "Vendido") && (x.CompradorEmail == emailUsuarioLogado) && (x.Avaliado == false));
                ViewBag.Comprador = anuncioListVendido;
                ViewBag.CompradorResult = anuncioListVendido.ToList().Count();
            }
            return View();
        }

        public ActionResult Filtro()
        {
            //var pesquisaProds = db.Anuncios.Where(x => (x.TituloAnuncio == "TituloAnuncio")).ToList();
            return View();
        }

        // POST: Anuncio/Pesquisar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Filtro([Bind(Include = "TituloAnuncio,Valor")] Anuncio anuncio)
        {
            //metodo de seleçao 
            //consultar no banco a partir dos inputs  
            //ViewBag.TituloAnuncio = new SelectList(db.Anuncios, "TituloAununcio");
            //ViewBag.Valor = new SelectList(db.Anuncios, "Valor");
            var pesquisaProds = db.Anuncios.Where(p => (p.TituloAnuncio == "TituloAnuncio"));
            ViewBag.pesquisaProds = pesquisaProds;

            return View(pesquisaProds);
            //return RedirectToAction("Index");
        }


        // AJAX: /Anuncio/ResponderAnuncio/5
        [HttpPost]
        public ActionResult ResponderAnuncio(int id, string contentResposta, string pergunta)
        {
            IEnumerable<FAQ> faqs = db.Faqs.Where(x => (x.AnuncioID == id));
            FAQ fagTarget = faqs.Where(x => x.Pergunta == pergunta).First();
            fagTarget.Resposta = contentResposta;
            db.Entry(fagTarget).State = EntityState.Modified;
            db.SaveChanges();
            var results = new
            {
                Message = "A resposta foi enviada!",
            };
            return Json(results);
        }

        // AJAX: /Anuncio/FazerPergunta/5
        [HttpPost]
        [Authorize]
        public ActionResult FazerPergunta(int id, string conteudoPergunta)
        {
            FAQ faq = new FAQ { AnuncioID = id, Pergunta = conteudoPergunta };
            if (Request.IsAuthenticated)
            {
                string emailUsuario = User.Identity.GetUserName();
                faq = new FAQ { AnuncioID = id, Pergunta = conteudoPergunta, UsuarioEmail = emailUsuario };
            }
            db.Faqs.Add(faq);
            db.SaveChanges();
            var results = new
            {
                Message = "A pergunta foi enviada!",
            };
            return Json(results);
        }

        public ActionResult FecharNegocio(int id)
        {
            StatusAnuncio status = db.StatusAnuncio.Where(x => x.Descricao == "Vendido").First();
            int IdVendido = status.StatusID;
            Anuncio anuncio = db.Anuncios.Find(id);
            anuncio.StatusID = IdVendido;
            db.Entry(anuncio).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ListNotificacoesUsuario");
        }

        public ActionResult DesistirVenda(int id)
        {
            StatusAnuncio status = db.StatusAnuncio.Where(x => x.Descricao == "Desistencia de venda").First();
            int IdVendido = status.StatusID;
            Anuncio anuncio = db.Anuncios.Find(id);
            anuncio.StatusID = IdVendido;
            db.Entry(anuncio).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ListNotificacoesUsuario");
        }

        public ActionResult AvaliarAnuncio(int id, int countStar)
        {
            Anuncio anuncio = db.Anuncios.Find(id);
            anuncio.numeroEstrelas = countStar;
            anuncio.Avaliado = true;
            db.Entry(anuncio).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ListNotificacoesUsuario");
        }
    }
}
