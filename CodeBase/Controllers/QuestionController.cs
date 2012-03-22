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
    public class QuestionController : Controller
    {
        private CodeBaseContext db = new CodeBaseContext();

        //
        // GET: /Question/

        public ViewResult Index()
        {
            var questions = db.Questions.Include(q => q.Author);
            return View(questions.ToList());
        }

        //
        // GET: /Question/Details/5

        public ViewResult Details(int id)
        {
            Question question = db.Questions.Find(id);
            return View(question);
        }

        //
        // GET: /Question/Create

        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username");
            return View();
        } 

        //
        // POST: /Question/Create

        [HttpPost]
        public ActionResult Create(Question question)
        {
            if (ModelState.IsValid)
            {
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", question.UserId);
            return View(question);
        }
        
        //
        // GET: /Question/Edit/5
 
        public ActionResult Edit(int id)
        {
            Question question = db.Questions.Find(id);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", question.UserId);
            return View(question);
        }

        //
        // POST: /Question/Edit/5

        [HttpPost]
        public ActionResult Edit(Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", question.UserId);
            return View(question);
        }

        //
        // GET: /Question/Delete/5
 
        public ActionResult Delete(int id)
        {
            Question question = db.Questions.Find(id);
            return View(question);
        }

        //
        // POST: /Question/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
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