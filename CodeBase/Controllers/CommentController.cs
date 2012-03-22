﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeBase.Models;

namespace CodeBase.Controllers
{ 
    public class CommentController : Controller
    {
        private CodeBaseContext db = new CodeBaseContext();

        //
        // GET: /Comment/

        public ViewResult Index()
        {
            var comments = db.Comments.Include(c => c.Author).Include(c => c.Article);
            return View(comments.ToList());
        }

        //
        // GET: /Comment/Details/5

        public ViewResult Details(int id)
        {
            Comment comment = db.Comments.Find(id);
            return View(comment);
        }

        //
        // GET: /Comment/Create

        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username");
            ViewBag.ArticleId = new SelectList(db.Articles, "ArticleId", "Title");
            return View();
        } 

        //
        // POST: /Comment/Create

        [HttpPost]
        public ActionResult Create(Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", comment.UserId);
            ViewBag.ArticleId = new SelectList(db.Articles, "ArticleId", "Title", comment.ArticleId);
            return View(comment);
        }
        
        //
        // GET: /Comment/Edit/5
 
        public ActionResult Edit(int id)
        {
            Comment comment = db.Comments.Find(id);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", comment.UserId);
            ViewBag.ArticleId = new SelectList(db.Articles, "ArticleId", "Title", comment.ArticleId);
            return View(comment);
        }

        //
        // POST: /Comment/Edit/5

        [HttpPost]
        public ActionResult Edit(Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", comment.UserId);
            ViewBag.ArticleId = new SelectList(db.Articles, "ArticleId", "Title", comment.ArticleId);
            return View(comment);
        }

        //
        // GET: /Comment/Delete/5
 
        public ActionResult Delete(int id)
        {
            Comment comment = db.Comments.Find(id);
            return View(comment);
        }

        //
        // POST: /Comment/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}