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
    public class RatingController : Controller
    {
        private CodeBaseContext db = new CodeBaseContext();

        //
        // GET: /Rating/

        public ViewResult Index()
        {
            var ratings = db.Ratings.Include(r => r.Author).Include(r => r.Article);
            return View(ratings.ToList());
        }

        //
        // GET: /Rating/Details/5

        public ViewResult Details(int id)
        {
            Rating rating = db.Ratings.Find(id);
            return View(rating);
        }

        //
        // GET: /Rating/Create

        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username");
            ViewBag.ArticleId = new SelectList(db.Articles, "ArticleId", "Title");
            return View();
        } 

        //
        // POST: /Rating/Create

        [HttpPost]
        public ActionResult Create(Rating rating)
        {
            if (ModelState.IsValid)
            {
                db.Ratings.Add(rating);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", rating.UserId);
            ViewBag.ArticleId = new SelectList(db.Articles, "ArticleId", "Title", rating.ArticleId);
            return View(rating);
        }
        
        //
        // GET: /Rating/Edit/5
 
        public ActionResult Edit(int id)
        {
            Rating rating = db.Ratings.Find(id);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", rating.UserId);
            ViewBag.ArticleId = new SelectList(db.Articles, "ArticleId", "Title", rating.ArticleId);
            return View(rating);
        }

        //
        // POST: /Rating/Edit/5

        [HttpPost]
        public ActionResult Edit(Rating rating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", rating.UserId);
            ViewBag.ArticleId = new SelectList(db.Articles, "ArticleId", "Title", rating.ArticleId);
            return View(rating);
        }

        //
        // GET: /Rating/Delete/5
 
        public ActionResult Delete(int id)
        {
            Rating rating = db.Ratings.Find(id);
            return View(rating);
        }

        //
        // POST: /Rating/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Rating rating = db.Ratings.Find(id);
            db.Ratings.Remove(rating);
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