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
    public class AnswersController : Controller
    {
        private CodeBaseContext context = new CodeBaseContext();

        //
        // GET: /Answers/

        public ViewResult Index()
        {
            return View(context.Answers.Include(answer => answer.Question).ToList());
        }

        //
        // GET: /Answers/Details/5

        public ViewResult Details(int id)
        {
            Answer answer = context.Answers.Single(x => x.AnswerId == id);
            return View(answer);
        }

        //
        // GET: /Answers/Create

        public ActionResult Create()
        {
            ViewBag.PossibleUsers = context.Users;
            ViewBag.PossibleQuestions = context.Questions;
            return View();
        } 

        //
        // POST: /Answers/Create

        [HttpPost]
        public ActionResult Create(Answer answer)
        {
            if (ModelState.IsValid)
            {
                context.Answers.Add(answer);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.PossibleUsers = context.Users;
            ViewBag.PossibleQuestions = context.Questions;
            return View(answer);
        }
        
        //
        // GET: /Answers/Edit/5
 
        public ActionResult Edit(int id)
        {
            Answer answer = context.Answers.Single(x => x.AnswerId == id);
            ViewBag.PossibleUsers = context.Users;
            ViewBag.PossibleQuestions = context.Questions;
            return View(answer);
        }

        //
        // POST: /Answers/Edit/5

        [HttpPost]
        public ActionResult Edit(Answer answer)
        {
            if (ModelState.IsValid)
            {
                context.Entry(answer).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PossibleUsers = context.Users;
            ViewBag.PossibleQuestions = context.Questions;
            return View(answer);
        }

        //
        // GET: /Answers/Delete/5
 
        public ActionResult Delete(int id)
        {
            Answer answer = context.Answers.Single(x => x.AnswerId == id);
            return View(answer);
        }

        //
        // POST: /Answers/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Answer answer = context.Answers.Single(x => x.AnswerId == id);
            context.Answers.Remove(answer);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}