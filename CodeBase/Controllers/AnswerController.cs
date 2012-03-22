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
    public class AnswerController : Controller
    {
        private CodeBaseContext db = new CodeBaseContext();

        //
        // GET: /Answer/

        public ViewResult Index()
        {
            var answers = db.Answers.Include(a => a.Author).Include(a => a.Question);
            return View(answers.ToList());
        }

        //
        // GET: /Answer/Details/5

        public ViewResult Details(int id)
        {
            Answer answer = db.Answers.Find(id);
            return View(answer);
        }

        //
        // GET: /Answer/Create

        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username");
            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "Content");
            return View();
        } 

        //
        // POST: /Answer/Create

        [HttpPost]
        public ActionResult Create(Answer answer)
        {
            if (ModelState.IsValid)
            {
                db.Answers.Add(answer);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", answer.UserId);
            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "Content", answer.QuestionId);
            return View(answer);
        }
        
        //
        // GET: /Answer/Edit/5
 
        public ActionResult Edit(int id)
        {
            Answer answer = db.Answers.Find(id);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", answer.UserId);
            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "Content", answer.QuestionId);
            return View(answer);
        }

        //
        // POST: /Answer/Edit/5

        [HttpPost]
        public ActionResult Edit(Answer answer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(answer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", answer.UserId);
            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "Content", answer.QuestionId);
            return View(answer);
        }

        //
        // GET: /Answer/Delete/5
 
        public ActionResult Delete(int id)
        {
            Answer answer = db.Answers.Find(id);
            return View(answer);
        }

        //
        // POST: /Answer/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Answer answer = db.Answers.Find(id);
            db.Answers.Remove(answer);
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