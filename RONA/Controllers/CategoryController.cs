using Microsoft.AspNetCore.Mvc;
using SiteRONA.Data;
using SiteRONA.Models;

namespace SiteRONA.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db) 
        {
            _db= db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (_db.Categories.Any(u => u.Name == obj.Name || u.DisplayOrder == obj.DisplayOrder))
            {
                if (_db.Categories.Any(u => u.Name == obj.Name))
                {
                    ModelState.AddModelError("Name", "Ce nom de catégorie existe déjà");
                }
                if (_db.Categories.Any(u => u.DisplayOrder == obj.DisplayOrder))
                {
                    ModelState.AddModelError("DisplayOrder", "Cet ordre d'affichage existe déjà");
                }
                return View();
            }
            if(ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["message"] = $"La catégorie {obj.Name} a été ajoutée avec succès";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (_db.Categories.Any(u => u.Name == obj.Name))
            {
                if (_db.Categories.Any(u => u.Name == obj.Name))
                {
                    ModelState.AddModelError("Name", "Ce nom de catégorie existe déjà");
                }
               
                return View();
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["message"] = $"La catégorie {obj.Name} a été modifiée avec succès";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Delete(Category obj)
        {
            
            if (ModelState.IsValid)
            {
                _db.Categories.Remove(obj);
                _db.SaveChanges();
                TempData["message"] = $"La catégorie {obj.Name} a été supprimée avec succès";
                return RedirectToAction("Index");
            }
            return View();
        }


    }
}
