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
using System.ServiceModel.Syndication;
using Rotativa;
using CodeBase.ViewModel;
using AutoMapper;

namespace CodeBase.Controllers
{
    public class ArticlesController : Controller
    {
        public CodeBaseContext context = new CodeBaseContext();
        public ICodeBaseMembership membership = new CodeBaseMembership();

        [HttpPost, ActionName("Rate")]
        [Authorize]
        public ActionResult Rate(int id, float value)
        {
            String currentUser = membership.LoggedInUser();
            User u = context.Users.Where(x => x.Username == currentUser).First();
            Rating r = context.Ratings.Where(x => x.ArticleId == id && x.UserId == u.UserId).FirstOrDefault();
            if (r == null)
                context.Ratings.Add(new Rating { ArticleId = id, UserId = u.UserId, Date = DateTime.Now, Value = (int)value });
            else
            {
                r.Value = (int)value;
            }
            context.SaveChanges();

            return Json(new { data = ModelHelpers.AverageRating(context.Articles.Find(id)) });
        }

        public ActionResult Feed()
        {
            var articles = context.Articles.OrderBy(pub => pub.Date).Take(15).ToList().Select(p => new SyndicationItem(p.Title, BBCodeHelper.Format(p.Content), new Uri(Url.Action("Details", "Articles", new { id = p.ArticleId }, "http")).SetPort(80)));

            var feed = new SyndicationFeed("CodeBase", "Your source to knowledge", new Uri(Url.Action("Index", "Home", new { }, "http")).SetPort(80), articles);

            return new FeedResult(new Rss20FeedFormatter(feed));


        }

        [HttpPost, Authorize]
        public ActionResult AddComment(Comment c)
        {
            String currentUser = membership.LoggedInUser();
            c.UserId = context.Users.Where(x => x.Username == currentUser).Single().UserId;
            c.Date = DateTime.Now;
            if (TryValidateModel(c))
            {
                context.Comments.Add(c);
                context.SaveChanges();

                TempData["Message"] = "Comment created.";
                return Redirect(Request.UrlReferrer.ToString());
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        //
        // GET: /Articles/

        public ViewResult Index()
        {
            if (ModelHelpers.isEditor())
            {
                return View(context.Articles.Include(article => article.Category).Include(article => article.Ratings).Include(article => article.Comments).Include(article => article.Files).ToList());
            }
            else
            {
                return View(context.Articles.Where(x=>x.Approved==true).Include(article => article.Category).Include(article => article.Ratings).Include(article => article.Comments).Include(article => article.Files).ToList());
            }

        }


        public ViewResult EditorMode()
        {
            return View(context.Articles.Include(article => article.Category).Include(article => article.Ratings).Include(article => article.Comments).Include(article => article.Files).ToList());
        }

        [Authorize]
        [ValidateInput(false)]
        public ActionResult Preview(String data)
        {
            return Content(BBCodeHelper.Format(data));
        }

        public ActionResult Pdf(int id)
        {
            return new ActionAsPdf("Details", new { id = id });
        }

        //
        // GET: /Articles/Details/5

        public ActionResult Details(int id, String title)
        {
            Article article = context.Articles.Include(x=>x.Comments).Include(x => x.Comments).Single(x => x.ArticleId == id);
            if (article.Approved==false && ModelHelpers.canEdit(article) == false)
            {
                TempData["Error"] = "Access denied";
                return RedirectToAction("Index");
            }

            if (Request.IsAuthenticated)
            {
                String username = membership.LoggedInUser();
                User u = context.Users.Single(x => x.Username == username);
                ViewData["subscribed"] = u.SubscriptionArticles.Any(x => x.ArticleId == id) ? "yes" : "no";
            }

            string realTitle = UrlEncoder.ToFriendlyUrl(article.Title);
            string urlTitle = (title ?? "").Trim().ToLower();
            if (realTitle != urlTitle)
            {
                string url = "/Articles/" + article.ArticleId + "/" + realTitle;
                return Redirect(url);
            }
            ViewBag.Rating = ModelHelpers.AverageRating(article);


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



        [HttpPost, ValidateInput(false)]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticleEditModel article)
        {


            Article a = Mapper.Map<ArticleEditModel, Article>(article);
            if (ModelState.IsValid)
            {
                a.Date = DateTime.Now;
                String currentUser = membership.LoggedInUser();
                a.Approved = autoApprove(context.Users.Where(x => x.Username == currentUser).FirstOrDefault());
                a.UserId = context.Users.FirstOrDefault(x => x.Username == currentUser).UserId;
                context.Articles.Add(a);
                context.SaveChanges();
                TempData["Message"] = "Article created.";
                return RedirectToAction("Details", new { id = a.ArticleId });
            }

            ViewBag.PossibleUsers = context.Users;
            ViewBag.PossibleCategories = context.Categories;
            return View(a);
        }

        //
        // GET: /Articles/Edit/5


        [Authorize]
        public ActionResult Edit(int id)
        {
            Article article = context.Articles.Single(x => x.ArticleId == id);
            if (ModelHelpers.canEdit(article))
            {
                ViewBag.PossibleCategories = context.Categories;
                return View(article);
            }
            TempData["Error"] = "Not authorized";
            return Redirect(Request.UrlReferrer.ToString());
        }

        [Authorize]
        public ActionResult DeleteComment(int id)
        {
            Comment c = context.Comments.Find(id);
            if (ModelHelpers.canEdit(c))
            {
                context.Comments.Remove(c);
                context.SaveChanges();
                TempData["Message"] = "Comment deleted.";
                return Redirect(Request.UrlReferrer.ToString());
            }
            TempData["Error"] = "Not authorized";
            return Redirect(Request.UrlReferrer.ToString());
        }

        //
        // POST: /Articles/Edit/5




        [HttpPost, ValidateInput(false)]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArticleEditModel editModel)
        {
            var article = context.Articles.Find(editModel.ArticleId);
            article = Mapper.Map<ArticleEditModel, Article>(editModel, article);
            if (ModelHelpers.canEdit(article))
            {
                if (ModelState.IsValid)
                {
                    context.Entry(article).State = EntityState.Modified;
                    context.SaveChanges();

                    TempData["Message"] = "Article edited.";
                    return RedirectToAction("Index");
                }
                ViewBag.PossibleUsers = context.Users;
                ViewBag.PossibleCategories = context.Categories;
                return View(article);
            }

            TempData["Error"] = "Not authorized";
            return Redirect(Request.UrlReferrer.ToString());
        }


        //
        // GET: /Articles/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {

            Article article = context.Articles.Single(x => x.ArticleId == id);
            if (ModelHelpers.canEdit(article))
            {
                return View(article);
            }

            TempData["Error"] = "Not authorized";
            return Redirect(Request.UrlReferrer.ToString());
        }

        //
        // POST: /Articles/Delete/5

        [HttpPost, ActionName("Delete")]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {

            Article article = context.Articles.Single(x => x.ArticleId == id);
            if (ModelHelpers.canEdit(article))
            {
                foreach (Rating r in context.Ratings.Where(x => x.ArticleId == article.ArticleId))
                {
                    context.Ratings.Remove(r);
                }
                foreach (File f in context.Files.Where(x => x.ArticleId == article.ArticleId))
                {
                    context.Files.Remove(f);
                }
                foreach (Comment c in context.Comments.Where(x => x.ArticleId == article.ArticleId))
                {
                    context.Comments.Remove(c);
                }
                context.Articles.Remove(article);
                context.SaveChanges();
                TempData["Message"] = "Article was successfully deleted.";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Not authorized";
            return Redirect(Request.UrlReferrer.ToString());
        }

        private bool autoApprove(User u)
        {
            //Editor status or higher
            if (Roles.GetRolesForUser().Intersect(new String[] { "Admin", "Editor" }).Count() > 0)
            {
                return true;
            }
            else if (u.Articles.Count() >= 5)
            {
                return true;
            }
            return false;
        }

        [Authorize(Roles = "Editor, Admin")]
        public ActionResult ConfirmArticle(int articleId)
        {
            Article a = context.Articles.Find(articleId);
            if (a != null)
            {
                a.Approved = true;
                context.SaveChanges();
            }
            TempData["Message"] = "Article " + a.Title + " successfully approved.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public String Subscribe(FormCollection form)
        {
            int aid = Convert.ToInt32(form["articleid"]);
            Article a = context.Articles.SingleOrDefault(x => x.ArticleId == aid);
            if (a == null)
                return "";

            String user = membership.LoggedInUser();
            User uObj = context.Users.Single(x => x.Username == user);
            bool found = false;
            ICollection<Article> subscriptions = uObj.SubscriptionArticles.ToList();

            foreach (Article acurrent in subscriptions)
            {
                if (acurrent.ArticleId == aid) // already subscribed, unsubscribe
                {
                    found = true;
                    context.Users.Single(x => x.Username == user).SubscriptionArticles.Remove(acurrent);
                    context.SaveChanges();
                    return "deleted";
                }
            }

            if (!found)
            {
                context.Users.Single(x => x.Username == user).SubscriptionArticles.Add(a);
                context.SaveChanges();
            }
            return "subscribed";
        }
        
        //
        // GET: /Articles/ByUser/5

        [Authorize]
        public ActionResult ByUser(int id)
        {
            ViewData["UserName"] = context.Users.Single(x => x.UserId == id).Username;
            return View("ArticlesByUser",context.Users.Single(x=>x.UserId==id).Articles);
        }
    }

}