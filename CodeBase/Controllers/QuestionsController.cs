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
    public class QuestionsController : Controller
    {
        private CodeBaseContext context = new CodeBaseContext();

        //
        // GET: /Questions/

        public ViewResult Index()
        {
            return View(context.Questions.Include(question => question.Answers).ToList());
        }

        //
        // GET: /Questions/Details/5

        public ViewResult Details(int id)
        {
            Question question = context.Questions.Single(x => x.QuestionId == id);
            return View(question);
        }

        //
        // GET: /Questions/Create

        public ActionResult Create()
        {
            ViewBag.PossibleUsers = context.Users;
            return View();
        } 

        //
        // POST: /Questions/Create

        [HttpPost]
        public ActionResult Create(Question question)
        {
            if (ModelState.IsValid)
            {
                context.Questions.Add(question);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.PossibleUsers = context.Users;
            return View(question);
        }
        
        //
        // GET: /Questions/Edit/5
 
        public ActionResult Edit(int id)
        {
            Question question = context.Questions.Single(x => x.QuestionId == id);
            ViewBag.PossibleUsers = context.Users;
            return View(question);
        }

        //
        // POST: /Questions/Edit/5

        [HttpPost]
        public ActionResult Edit(Question question)
        {
            if (ModelState.IsValid)
            {
                context.Entry(question).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PossibleUsers = context.Users;
            return View(question);
        }

        //
        // GET: /Questions/Delete/5
 
        public ActionResult Delete(int id)
        {
            Question question = context.Questions.Single(x => x.QuestionId == id);
            return View(question);
        }

        //
        // POST: /Questions/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = context.Questions.Single(x => x.QuestionId == id);
            context.Questions.Remove(question);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}