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
    public class FileController : Controller
    {
        private CodeBaseContext db = new CodeBaseContext();

        //
        // GET: /File/

        public ViewResult Index()
        {
            var files = db.Files.Include(f => f.Article);
            return View(files.ToList());
        }

        //
        // GET: /File/Details/5

        public ViewResult Details(int id)
        {
            File file = db.Files.Find(id);
            return View(file);
        }

        //
        // GET: /File/Create

        public ActionResult Create()
        {
            ViewBag.ArticleId = new SelectList(db.Articles, "ArticleId", "Title");
            return View();
        } 

        //
        // POST: /File/Create

        [HttpPost]
        public ActionResult Create(File file)
        {
            if (ModelState.IsValid)
            {
                db.Files.Add(file);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.ArticleId = new SelectList(db.Articles, "ArticleId", "Title", file.ArticleId);
            return View(file);
        }
        
        //
        // GET: /File/Edit/5
 
        public ActionResult Edit(int id)
        {
            File file = db.Files.Find(id);
            ViewBag.ArticleId = new SelectList(db.Articles, "ArticleId", "Title", file.ArticleId);
            return View(file);
        }

        //
        // POST: /File/Edit/5

        [HttpPost]
        public ActionResult Edit(File file)
        {
            if (ModelState.IsValid)
            {
                db.Entry(file).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArticleId = new SelectList(db.Articles, "ArticleId", "Title", file.ArticleId);
            return View(file);
        }

        //
        // GET: /File/Delete/5
 
        public ActionResult Delete(int id)
        {
            File file = db.Files.Find(id);
            return View(file);
        }

        //
        // POST: /File/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            File file = db.Files.Find(id);
            db.Files.Remove(file);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}