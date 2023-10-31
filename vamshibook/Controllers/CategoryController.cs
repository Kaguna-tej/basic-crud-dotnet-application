using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.ComponentModel.DataAnnotations;
using vamshibook.Data;
using vamshibook.Models;

namespace vamshibook.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {

            return View();
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Category obj)

        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The display order should not match to the name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Successfully created";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var categoryfromFind = _db.Categories.Find(id);
            //var categoryFirst = _db.Categories.FirstOrDefault(c => c.Id == id);
            //var categorySingle = _db.Categories.SingleOrDefault(c => c.Id == id);
            if(categoryfromFind == null)
            {
                return NotFound();
            }
                    return View(categoryfromFind);
        }
        //post edit
        [HttpPost]

        [ValidateAntiForgeryToken]

        public IActionResult Edit(Category obj) 
        {
            if (obj.Name == obj.DisplayOrder.ToString()) 
            {
                ModelState.AddModelError("name", "the name should not match display order!!");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Successfully Updated";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var categoryfromFind = _db.Categories.Find(id);
            //var categoryFirst = _db.Categories.FirstOrDefault(c => c.Id == id);
            //var categorySingle = _db.Categories.SingleOrDefault(c => c.Id == id);
            if (categoryfromFind == null)
            {
                return NotFound();
            }
            return View(categoryfromFind);
        }
        //post Delete

        [HttpPost]

        [ValidateAntiForgeryToken]

        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Successfully deleted";
            return RedirectToAction("Index");
        }

    }
}