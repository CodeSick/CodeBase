using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CodeBase.Models;
using CodeBase.ViewModel;

namespace CodeBase.Controllers
{   
    public class QuestionsController : Controller
    {
        private CodeBaseContext context = new CodeBaseContext();
        public ICodeBaseMembership membership = new CodeBaseMembership();

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
            QAViewModel vm = new QAViewModel();
            Question question = context.Questions.Single(x => x.QuestionId == id);
            ViewData["username"] = "-1";

            try
            {
                ViewData["username"] = membership.LoggedInUser();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            vm.question = question;
            return View(vm);
        }

        //
        // GET: /Questions/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Questions/Create

        [HttpPost]
        [Authorize]
        public ActionResult Create(Question question)
        {
            question.Date = DateTime.Now;
            String u = Membership.GetUser().UserName;
            question.UserId = context.Users.Single(x => x.Username == u).UserId;

            if (ModelState.IsValid)
            {
                context.Questions.Add(question);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(question);
        }
        
        //
        // GET: /Questions/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            Question question = context.Questions.Single(x => x.QuestionId == id);
            return View(question);
        }

        //
        // POST: /Questions/Edit/5

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Question question)
        {
            Question q = context.Questions.Single(x => x.QuestionId == question.QuestionId);
            question.UserId = q.UserId;
            question.Date = q.Date;

            if (ModelState.IsValid)
            {
                context.Entry(q).CurrentValues.SetValues(question);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PossibleUsers = context.Users;
            return View(question);
        }

        //
        // GET: /Questions/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            Question question = context.Questions.Single(x => x.QuestionId == id);
            return View(question);
        }

        //
        // POST: /Questions/Delete/5

        [HttpPost, ActionName("Delete")]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = context.Questions.Single(x => x.QuestionId == id);

            foreach (Answer answer in context.Answers.Where(x => x.QuestionId == question.QuestionId))
            {
                context.Answers.Remove(answer);
            }

            context.Questions.Remove(question);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}