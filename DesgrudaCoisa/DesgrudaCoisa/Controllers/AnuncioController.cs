using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DesgrudaCoisa.Models;

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
            Anuncio anuncio = db.Anuncios.Find(id);
            if (anuncio == null)
            {
                return HttpNotFound();
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
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,TituloAnuncio,Valor,DataPublicacao,CategoriaID,Imagem")] Anuncio anuncio)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Anuncios.Add(anuncio);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "TituloCategoria", anuncio.CategoriaID);
        //    return View(anuncio);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(String tituloAnuncio, String descricao, decimal valor, int categoriaID, HttpPostedFileBase image)
        {
            Imagem imagem = new Imagem { ImageFile = new byte[image.ContentLength], ImageMimeType = image.ContentType};
            image.InputStream.Read(imagem.ImageFile, 0, image.ContentLength);
            db.Imagens.Add(imagem);
            db.SaveChanges();
            Anuncio anuncio = new Anuncio
            {
                TituloAnuncio = tituloAnuncio,
                Valor = valor,
                Descricao = descricao,
                EnumStatusID = 1,
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
        public ActionResult Edit([Bind(Include = "ID,TituloAnuncio,Valor,DataPublicacao,CategoriaID")] Anuncio anuncio)
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

        // POST: Anuncio/FiltroAvancado
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PesquisarFiltro([Bind(Include = "TituloAnuncio,Valor")] Anuncio anuncio)
        {
            //metodo de seleçao 
            //consultar no banco a partir dos inputs  
            ViewBag.ID = new SelectList(db.Anuncios, "ID", "TituloAununcio");
            return View();
            //return RedirectToAction("Index");
        }
    }
}
