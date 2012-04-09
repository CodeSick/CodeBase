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
        public CodeBaseMembership membership = new CodeBaseMembership();

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
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.PossibleUsers = context.Users;
            ViewBag.PossibleQuestions = context.Questions;
            return View();
        } 

        //
        // POST: /Answers/Create

        [HttpPost]
        [Authorize]
        public ActionResult Create(Answer answer, FormCollection form)
        {
            answer.Date = DateTime.Now;
            String u = membership.LoggedInUser();
            answer.UserId = context.Users.Single(x => x.Username == u).UserId;
            answer.QuestionId = Convert.ToInt32(form["answer_QuestionId"]);
            Question findQ = context.Questions.SingleOrDefault(x => x.QuestionId == answer.QuestionId);

            if (answer.Content != null && findQ != null)
            {
                if (answer.Content.Length > 5)
                {
                    context.Answers.Add(answer);
                    context.SaveChanges();
                    return RedirectToAction("Details/" + answer.QuestionId, "Questions");
                }
            }

            ViewBag.PossibleUsers = context.Users;
            ViewBag.PossibleQuestions = context.Questions;
            return RedirectToAction("Details/" + answer.QuestionId, "Questions");
        }
        
        //
        // GET: /Answers/Edit/5
        [Authorize]
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
        [Authorize]
        public ActionResult Edit(Answer answer)
        {
            Answer a = context.Answers.Single(x => x.AnswerId == answer.AnswerId);

            if (answer.Content != null && answer.Content.Length > 5)
            {
                answer.UserId = a.UserId;
                answer.Date = a.Date;
                answer.QuestionId = a.QuestionId;
                answer.Author = a.Author;
                answer.Question = a.Question;
                context.Entry(a).CurrentValues.SetValues(answer);
                context.SaveChanges();
                return RedirectToAction("Details/" + answer.QuestionId, "Questions");
            }

            ViewBag.PossibleUsers = context.Users;
            ViewBag.PossibleQuestions = context.Questions;
            return View(answer);
        }

        //
        // GET: /Answers/Delete/5

        [Authorize]
        public ActionResult Delete(int id)
        {
            Answer answer = context.Answers.Single(x => x.AnswerId == id);
            return View(answer);
        }

        //
        // POST: /Answers/Delete/5

        [HttpPost, ActionName("Delete")]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Answer answer = context.Answers.Single(x => x.AnswerId == id);
            context.Answers.Remove(answer);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}