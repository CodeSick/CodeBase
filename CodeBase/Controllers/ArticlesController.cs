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
using System.Net;
using System.Globalization;

namespace CodeBase.Controllers
{   
    public class ArticlesController : Controller
    {
        private CodeBaseContext context = new CodeBaseContext();

        [HttpPost, ActionName("Rate")]
        [Authorize]
        public ActionResult Rate(int id, float value)
        {
            MembershipUser currentUser = Membership.GetUser();
            User u = context.Users.Where(x => x.Username == currentUser.UserName).First();
            Rating r = context.Ratings.Where(x => x.ArticleId == id && x.UserId == u.UserId).FirstOrDefault();
            if (r == null)
                context.Ratings.Add(new Rating { ArticleId = id, UserId = u.UserId, Date = DateTime.Now, Value = (int)value });
            else
            {
                r.Value = (int)value;
            }
            context.SaveChanges();

            return  Json(new { data = AverageRating(id) });
        }

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

        private float AverageRating(int id)
        {
            IEnumerable<Rating> ratings = context.Ratings.Where(x => x.ArticleId == id);
            float average = (float)ratings.Sum(x => x.Value) / ratings.Count();
            return average;
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
            ViewBag.Rating = AverageRating(article.ArticleId);

            return View(article);
        }

        //
        // GET: /Articles/Create


        [Authorize]
        public ActionResult Create()
        {
            ViewBag.PossibleUsers = context.Users;
            ViewBag.PossibleCategories = context.Categories;
            return View();
        } 

        //
        // POST: /Articles/Create



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