using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeBase.ViewModel;

namespace CodeBase.Controllers
{
    public class HomeController : Controller
    {
        CodeBase.Models.CodeBaseContext context = new Models.CodeBaseContext();
        public ActionResult Index()
        {
            IndexViewModel model = new IndexViewModel { Message = "Hello to this beautiful site", Articles = context.Articles.Take(5).ToList() };

            return View(model);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
