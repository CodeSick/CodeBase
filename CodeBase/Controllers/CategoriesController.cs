using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeBase.Models;

namespace CodeBase.Controllers
{   
    public class CategoriesController : Controller
    {
        public CodeBaseContext context = new CodeBaseContext();

        //
        // GET: /Categories/

        public ViewResult Index()
        {
            return View(context.Categories.Include(category => category.Articles).ToList());
        }

        //
        // GET: /Categories/Details/5

        public ViewResult Details(int id)
        {
            Category category = context.Categories.Single(x => x.CategoryId == id);
            return View(category);
        }

        //
        // GET: /Categories/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Categories/Create

        [HttpPost]
        public ActionResult Create(Category category)
        {

            if (ModelState.IsValid)
            {
                context.Categories.Add(category);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(category);
        }
        
        //
        // GET: /Categories/Edit/5
 
        public ActionResult Edit(int id)
        {
            Category category = context.Categories.Single(x => x.CategoryId == id);
            return View(category);
        }

        //
        // POST: /Categories/Edit/5

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                context.Entry(category).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //
        // GET: /Categories/Delete/5
 
        public ActionResult Delete(int id)
        {
            Category category = context.Categories.Single(x => x.CategoryId == id);
            return View(category);
        }

        //
        // POST: /Categories/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = context.Categories.Single(x => x.CategoryId == id);
            context.Categories.Remove(category);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}