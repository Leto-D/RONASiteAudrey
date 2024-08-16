using Microsoft.AspNetCore.Mvc;
using Rona.DataAcces.Repository.InterfaceRepository;
using RONA.DataAccess.Data;
using RONA.Models;

namespace SiteRONA.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository db) 
        {
            _categoryRepo = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _categoryRepo.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (_categoryRepo.GetAll().Any(u => u.Name == obj.Name || u.DisplayOrder == obj.DisplayOrder))
            {
                if (_categoryRepo.GetAll().Any(u => u.Name == obj.Name))
                {
                    ModelState.AddModelError("Name", "Ce nom de catégorie existe déjà");
                }
                if (_categoryRepo.GetAll().Any(u => u.DisplayOrder == obj.DisplayOrder))
                {
                    ModelState.AddModelError("DisplayOrder", "Cet ordre d'affichage existe déjà");
                }
                return View();
            }
            if(ModelState.IsValid)
            {
                _categoryRepo.Add(obj);
                _categoryRepo.Save();
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
            Category categoryFromDb = _categoryRepo.Get(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            
            if (ModelState.IsValid)
            {
                _categoryRepo.Update(obj);
                _categoryRepo.Save();
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
            Category? categoryFromDb = _categoryRepo.Get(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _categoryRepo.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _categoryRepo.Remove(obj);
                _categoryRepo.Save();
                TempData["message"] = $"La catégorie {obj.Name} a été supprimée avec succès";
                return RedirectToAction("Index");
            }
            return View();
        }


    }
}
