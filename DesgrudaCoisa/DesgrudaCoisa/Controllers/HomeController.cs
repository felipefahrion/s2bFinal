using DesgrudaCoisa.Models;
using System.Web.Mvc;
using System.Data.Entity;
using System.Linq;

namespace DesgrudaCoisa.Controllers
{
    public class HomeController : Controller
    {
        private MapeamentoEntidadesContext db = new MapeamentoEntidadesContext();

        public ActionResult Index()
        {
            var anuncios = this.db.Anuncios.Include(a => a.Categoria).Take(10).ToList();
            return View(anuncios);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}