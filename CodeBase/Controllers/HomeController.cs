using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeBase.Models;
using CodeBase.ViewModel;
using System.Data.Entity;

namespace CodeBase.Controllers
{
    public class HomeController : Controller
    {
        public ICodeBaseRepository context = new CodeBaseContext();

        public ActionResult Index()
        {
            IndexViewModel model = new IndexViewModel 
            { Message = "Hello to this beautiful site",
                Articles = context.Articles.Take(5).ToList(),
                Users= context.Users.OrderByDescending(x => x.Articles.Count).Take(5).Select(x => new UserWithCount{ User=x, Count=x.Articles.Count})
            };

            return View("Index",model);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
