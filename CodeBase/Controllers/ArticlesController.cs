using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using CodeBase.Models;
using System.Linq;

namespace CodeBase.Controllers
{   
    public class ArticlesController : Controller
    {
        private CodeBaseContext context;

        public ArticlesController(CodeBaseContext context)
        {
            this.context = context;
        }

        //
        // GET: /Articles/

        public ViewResult Index()
        {
            return View(context.Articles.Include(article => article.Category).Include(article => article.Ratings).Include(article => article.Comments).Include(article => article.Files).ToList());
        }

        public String Preview(String data)
        {
            return BBCodeHelper.Format(data);
        }

        //
        // GET: /Articles/Details/5

        public ViewResult Details(int id)
        {
            Article article = context.Articles.Single(x => x.ArticleId == id);

            return View(article);
        }

        //
        // GET: /Articles/Create

        [ValidateInput(false)]
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.PossibleUsers = context.Users;
            ViewBag.PossibleCategories = context.Categories;
            return View();
        } 

        //
        // POST: /Articles/Create


        [ValidateInput(false)]
        [HttpPost]
        [Authorize]
        public ActionResult Create(Article article)
        {
            article.Date = DateTime.Now;
            if (ModelState.IsValid)
            {
                context.Articles.Add(article);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.PossibleUsers = context.Users;
            ViewBag.PossibleCategories = context.Categories;
            return View(article);
        }
        
        //
        // GET: /Articles/Edit/5

        [ValidateInput(false)]
        [Authorize]
        public ActionResult Edit(int id)
        {
            Article article = context.Articles.Single(x => x.ArticleId == id);
            ViewBag.PossibleUsers = context.Users;
            ViewBag.PossibleCategories = context.Categories;
            return View(article);
        }

        //
        // POST: /Articles/Edit/5


        [ValidateInput(false)]
        [HttpPost]
        [Authorize]
        public ActionResult Edit(Article article)
        {
            if (ModelState.IsValid)
            {
                context.Entry(article).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PossibleUsers = context.Users;
            ViewBag.PossibleCategories = context.Categories;
            return View(article);
        }


        //
        // GET: /Articles/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            Article article = context.Articles.Single(x => x.ArticleId == id);
            return View(article);
        }

        //
        // POST: /Articles/Delete/5

        [HttpPost, ActionName("Delete")]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = context.Articles.Single(x => x.ArticleId == id);
            context.Articles.Remove(article);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}