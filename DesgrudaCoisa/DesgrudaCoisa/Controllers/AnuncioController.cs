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
            ViewBag.faqs = null;
            IEnumerable<FAQ> listFaq = db.Faqs.Where(x => x.AnuncioID == anuncio.AnuncioID).ToList();
            if (listFaq.Count() >= 1)
            {
                ViewBag.faqs = listFaq;
            }
            return View(anuncio);
        }

        // GET: Anuncio/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "TituloCategoria");
            return View();
        }

        // POST: Anuncio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AnuncioID,TituloAnuncio,Valor,DataPublicacao,CategoriaID,Imagem")] Anuncio anuncio)
        {
            if (ModelState.IsValid)
            {
                db.Anuncios.Add(anuncio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "TituloCategoria", anuncio.CategoriaID);
            return View(anuncio);
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

        public ActionResult Filtro()
        {
            return View();
        }

        // AJAX: /Anuncio/ComprarAnuncio/5
        [HttpPost]
        //[Authorize] -- TODO: DEPOIS COLOCAR DIREITINHO
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
                ViewBag.NumAnunciosNegociacao = numAnunciosNegociacao;
            }
            else
            {
                ViewBag.NumAnunciosNegociacao = 100;
            }
            return this.PartialView();
        }

        public ActionResult ListNotificacoesUsuario()
        {
            var anuncios = db.Anuncios.Include(a=>a.Status).Where(x => (x.Status.Descricao == "Disponivel"));
            //var anuncios = db.Anuncios.Include(a => a.Status).Where(x => (x.Status.Descricao == "Em negociacao"));
            //var anuncios = db.Anuncios.Include(a => a.Status).Where(x => (x.Status.Descricao == "Vendido"));
            return View(anuncios);
        }

        // POST: Anuncio/FiltroAvancado
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PesquisarFiltro([Bind(Include = "TituloAnuncio,Valor")] Anuncio anuncio)
        {
            //metodo de seleçao 
            //consultar no banco a partir dos inputs  
            ViewBag.ID = new SelectList(db.Anuncios, "AnuncioID", "TituloAununcio");
            return View();
            //return RedirectToAction("Index");
        }
    }
}
