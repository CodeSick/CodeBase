using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeBase.Models;

namespace CodeBase.Controllers
{   
    public class UsersController : Controller
    {
        private CodeBaseContext context = new CodeBaseContext();

        //
        // GET: /Users/

        public ViewResult Index()
        {
            return View(context.Users.Include(user => user.Articles).Include(user => user.Ratings).Include(user => user.Comments).Include(user => user.Answers).Include(user => user.Questions).ToList());
        }

        //
        // GET: /Users/Details/5

        public ViewResult Details(int id)
        {
            User user = context.Users.Single(x => x.UserId == id);
            return View(user);
        }

        //
        // GET: /Users/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Users/Create

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                context.Users.Add(user);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(user);
        }
        
        //
        // GET: /Users/Edit/5
 
        public ActionResult Edit(int id)
        {
            User user = context.Users.Single(x => x.UserId == id);
            return View(user);
        }

        //
        // POST: /Users/Edit/5

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //
        // GET: /Users/Delete/5
 
        public ActionResult Delete(int id)
        {
            User user = context.Users.Single(x => x.UserId == id);
            return View(user);
        }

        //
        // POST: /Users/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = context.Users.Single(x => x.UserId == id);
            context.Users.Remove(user);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}