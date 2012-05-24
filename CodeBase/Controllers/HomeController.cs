using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeBase.Models;
using CodeBase.ViewModel;
using System.Data.Entity;
using CodeBase.Helper;

namespace CodeBase.Controllers
{
    public class HomeController : Controller
    {
        public CodeBaseContext context = new CodeBaseContext();

        public ActionResult Index()
        {
            IndexViewModel model = new IndexViewModel 
            { Message = "Hello to this beautiful site",
                Articles = context.Articles.Where(x => x.Approved==true).Take(5).ToList().OrderByDescending(x => x.Date),
                Users= context.Users.OrderByDescending(x => x.Articles.Count).Take(5).Select(x => new UserWithCount{ User=x, Count=x.Articles.Count}).ToList(),
                Questions = context.Questions.OrderByDescending(x => x.Answers.Count).Take(5).Select(x => new QuestionsWithCount{ Question = x, Count = x.Answers.Count }).ToList(),
                ArticlesRating = context.Articles.OrderByDescending(x => x.Ratings.Sum( r => r.Value)/ x.Ratings.Count).Select(x => new ArticleRating { Article = x }).Take(5).ToList()
            };
            
            return View("Index",model);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
