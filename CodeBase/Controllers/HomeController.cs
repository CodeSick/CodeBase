using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeBase.Models;
using CodeBase.ViewModel;

namespace CodeBase.Controllers
{
    public class HomeController : Controller
    {
        ICodeBaseRepository context;
        public HomeController(ICodeBaseRepository repo)
        {
            context = repo;
        }

        public ActionResult Index()
        {
            IndexViewModel model = new IndexViewModel { Message = "Hello to this beautiful site", Articles = context.Articles.Take(5).ToList() };

            return View("Index",model);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
