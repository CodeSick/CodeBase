using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeBase.Models;

namespace CodeBase.Controllers
{
    public class SearchController : Controller
    {
        CodeBaseContext context = new CodeBaseContext();
        //
        // GET: /Search/

        public ActionResult SearchArticlesJson(String data, String format="json", int max=5)
        {
            var articles = context.Articles.Where(x => x.Approved==true && x.Title.ToLower().Contains(data)).Take(max);
            return Json(articles.Select(x => new { name = x.Title, id = x.ArticleId }), JsonRequestBehavior.AllowGet);
        }

    }
}
