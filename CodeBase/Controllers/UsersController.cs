using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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
            var i = context.Users.Include(user => user.Articles).Include(user => user.Ratings).Include(user => user.Comments).Include(user => user.Answers).Include(user => user.Questions).ToList();
            //i.ForEach(x => x.MembershipUser = Membership.GetUser(x.Username));
            //i.ForEach(x => x.Roles = Roles.GetRolesForUser(x.Username).ToList());
            return View(i);
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

        [Authorize(Roles="admin")]
        public ActionResult Edit(int id)
        {
            User user = context.Users.Single(x => x.UserId == id);
            return View(user);
        }

        //
        // POST: /Users/Edit/5


        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(User user, String role)
        {
            if (ModelState.IsValid)
            {
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
                var roles = Roles.GetRolesForUser(user.Username);
                if (role != "-1")
                {
                    if (roles.Length != 0)
                    {
                        Roles.RemoveUserFromRoles(user.Username, roles);
                    }
                    Roles.AddUserToRole(user.Username, role);
                }
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //
        // GET: /Users/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            User user = context.Users.Single(x => x.UserId == id);
            return View(user);
        }

        //
        // POST: /Users/Delete/5

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = context.Users.Single(x => x.UserId == id);
            Membership.DeleteUser(user.Username);
            context.Users.Remove(user);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: /Users/Settings

        [Authorize]
        public ViewResult Settings()
        {
            return View(context.Users.Single(x => x.Username == HttpContext.User.Identity.Name));
        }

        //
        // POST: /Users/EditSettings

        [Authorize]
        [HttpPost]
        public ActionResult EditSettings()
        {
            User user = context.Users.Single(x => x.Username == HttpContext.User.Identity.Name);
            string email = Request["email"];
            if (email == "" || email == null)
            {
                ModelState.AddModelError("email", "Email cannot be empty");
            }
            else
            {
                user.MembershipUser.Email=email;
                Membership.UpdateUser(user.MembershipUser);
            }
            return View("Settings",user);
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword()
        {
            User user = context.Users.Single(x => x.Username == HttpContext.User.Identity.Name);
            string oldpass = Request["oldpass"];
            string newpass = Request["newpass"];
            string newpass_confirm = Request["newpass_confirm"];
            if (oldpass == "")
            {
                ModelState.AddModelError("oldpass", "This field cannot be empty.");
            }
            if (newpass == "")
            {
                ModelState.AddModelError("newpass", "This field cannot be empty.");
            }
            if (newpass_confirm == "")
            {
                ModelState.AddModelError("newpass_confirm", "This field cannot be empty.");
            }
            if (oldpass != "" && newpass != "" && newpass_confirm != "")
            {
                if (newpass != newpass_confirm)
                {
                    ModelState.AddModelError("newpass_Confirm", "Passwords do not match.");
                }
                else
                {
                    if (newpass.Length >= 6)
                    {
                        if (user.MembershipUser.ChangePassword(oldpass, newpass))
                        {
                            Membership.UpdateUser(user.MembershipUser);
                        }
                        else
                        {
                            ModelState.AddModelError("oldpass", "Incorrect password.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("newpass", "Password to short, needs to have at least 6 characters");
                        ModelState.AddModelError("newpass_confirm", "Needs to have at least 6 characters");
                    }
                }
            }
            return View("Settings", user);
        }
    }
}