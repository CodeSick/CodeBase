using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using CodeBase.Models;
using System.Linq;
using System.Web.Security;
using CodeBase.Helper;

namespace CodeBase.Controllers
{   
    public class ArticlesController : Controller
    {
        private CodeBaseContext context = new CodeBaseContext();

        //
        // GET: /Articles/

        public ViewResult Index()
        {
            return View(context.Articles.Include(article => article.Category).Include(article => article.Ratings).Include(article => article.Comments).Include(article => article.Files).ToList());
        }

        public ActionResult Preview(String data)
        {
            return Content(BBCodeHelper.Format(data));
        }

        //
        // GET: /Articles/Details/5

        public ActionResult Details(int id, String title)
        {
            Article article = context.Articles.Single(x => x.ArticleId == id);

            string realTitle = UrlEncoder.ToFriendlyUrl(article.Title);
            string urlTitle = (title ?? "").Trim().ToLower();
            if (realTitle != urlTitle)
            {
                string url = "/Articles/" + article.ArticleId + "/" + realTitle;
                return Redirect(url);
            } 

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
            MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
            article.UserId = context.Users.FirstOrDefault(x => x.Username == currentUser.UserName).UserId;
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
                var a = context.Articles.Find(article.ArticleId);
                article.UserId = a.UserId;
                article.Date = a.Date;
                context.Entry(a).State = EntityState.Detached;

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