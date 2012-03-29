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

        public ActionResult SearchArticlesJson(String data)
        {
            return Json(context.Articles.Where(x => x.Title.ToLower().Contains(data)).Take(5).Select(x => new { name = x.Title, id = x.ArticleId }), JsonRequestBehavior.AllowGet);
        }

    }
}
