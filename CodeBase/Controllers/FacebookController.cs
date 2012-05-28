using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CodeBase.Models;
using Facebook;

namespace CodeBase.Controllers
{
    public class FacebookController : Controller
    {

        private CodeBaseContext context = new CodeBaseContext();
        public CodeBaseMembership membership = new CodeBaseMembership();

        [HttpPost]
        public String Login(FormCollection form)
        {
            dynamic fbuser;
            if (Session["accessToken"] == null)
            {
                Session["accessToken"] = form["accessToken"];
                FacebookClient c = new FacebookClient(form["accessToken"].ToString());
                User u = null;
                int fbuserid = -1;
                fbuser = c.Get("me");

                try
                {
                    fbuserid = Convert.ToInt32(fbuser.id);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                    
                u = context.Users.SingleOrDefault(x => x.FbId == fbuserid);

                if (u == null && fbuserid != -1)
                {
                    //generate nick and add user to db&membership
                    String name = fbuser.name + fbuserid;
                    
                    MembershipCreateStatus createStatus;
                    Membership.CreateUser(name, Session["accessToken"] as String, null, null, null, true, null, out createStatus);

                    if (createStatus == MembershipCreateStatus.Success)
                    {
                        User newfbuser = new User()
                        {
                            Username = name,
                            FbId = fbuserid,
                            JoinDate = DateTime.Now,
                        };

                        context.Users.Add(newfbuser);
                        context.SaveChanges();
                        Roles.AddUserToRole(newfbuser.Username, "Normal");
                        FormsAuthentication.SetAuthCookie(newfbuser.Username, true);
                        return newfbuser.Username;
                    }
                    else
                    {
                        return "error";
                    }
                }
                else if (u != null && fbuserid != -1)
                {
                    //login user with fb account
                    FormsAuthentication.SetAuthCookie(u.Username, true);
                    Session["user"] = context.Users.Where(x => x.Username == u.Username).ToList().FirstOrDefault();
                    
                    return u.Username;
                }
            }
            return "";
        }

        [HttpPost]
        public void Logout()
        {
            Session["accessToken"] = null;
            Session["user"] = null;
            FormsAuthentication.SignOut();
        }

    }
}
