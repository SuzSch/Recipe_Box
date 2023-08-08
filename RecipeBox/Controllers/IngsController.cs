using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace RecipeBox.Controllers
{
    public class IngsController : Controller
    {
        private readonly RecipeBoxContext _db;
        public IngsController(RecipeBoxContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Ingredients for YOU";
            List<Ing> model = _db.Ings.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.Title = "Add Ingredient";
            return View();
        }

        [HttpPost]
        public ActionResult Create(Ing ing)
        {
            _db.Add(ing);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Ing thisIng = _db.Ings.FirstOrDefault(ing => ing.IngId == id);
            _db.Ings.Remove(thisIng);
            _db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
