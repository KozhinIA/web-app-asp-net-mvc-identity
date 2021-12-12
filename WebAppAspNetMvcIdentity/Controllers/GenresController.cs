using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebAppAspNetMvcIdentity.Models;

namespace WebAppAspNetMvcIdentity.Controllers
{
    [Authorize]
    public class GenresController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var db = new LibraryContext();
            var genres = db.Genres.ToList();

            return View(genres);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            var genre = new Genre();
            return View(genre);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(Genre model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var db = new LibraryContext();                      

            db.Genres.Add(model);
            db.SaveChanges();

            return RedirectPermanent("/Genres/Index");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            var db = new LibraryContext();
            var genre = db.Genres.FirstOrDefault(x => x.Id == id);
            if (genre == null)
                return RedirectPermanent("/Genres/Index");

            db.Genres.Remove(genre);
            db.SaveChanges();

            return RedirectPermanent("/Genres/Index");
        }


        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            var db = new LibraryContext();
            var genre = db.Genres.FirstOrDefault(x => x.Id == id);
            if (genre == null)
                return RedirectPermanent("/Genres/Index");

            return View(genre);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(Genre model)
        {
            var db = new LibraryContext();
            var genre = db.Genres.FirstOrDefault(x => x.Id == model.Id);
            if (genre == null)
                ModelState.AddModelError("Id", "Жанр не найден");

            if (!ModelState.IsValid)
                return View(model);

            MappingGenre(model, genre);

            db.Entry(genre).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectPermanent("/Genres/Index");
        }

        private void MappingGenre(Genre sourse, Genre destination)
        {
            destination.Name = sourse.Name;
        }
    }
}