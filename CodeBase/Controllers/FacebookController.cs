using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public void Login(FormCollection form)
        {
            if (Session["accessToken"] == null)
            {

                Session["accessToken"] = form["accessToken"];
                FacebookClient c = new FacebookClient(form["accessToken"].ToString());
                dynamic fbuser;
                User u = null;
                String fbusername = null;
                int fbuserid;

                try
                {
                    fbuser = c.Get("me");
                    fbuserid = Convert.ToInt32(fbuser.id);
                    fbusername = fbuser.name + " " + fbuser.surname;
                    u = context.Users.SingleOrDefault(x => x.FbId == fbuserid);

                    if (u == null && fbusername != null)
                    {
                        MembershipCreateStatus createStatus;
                        Membership.CreateUser(fbusername, form["accessToken"], null, null, null, true, null, out createStatus);

                        if (createStatus == MembershipCreateStatus.Success)
                        {
                            User newfbuser = new User()
                            {
                                Username = fbuser.name + " " + fbuser.surname,
                                FbId = fbuserid,
                                JoinDate = DateTime.Now,
                            };

                            context.Users.Add(newfbuser);
                            context.SaveChanges();
                            Roles.AddUserToRole(newfbuser.Username, "Normal");
                            FormsAuthentication.SetAuthCookie(newfbuser.Username, true);
                        }
                    }
                    else if (u != null && fbusername != null)
                    {
                        FormsAuthentication.SetAuthCookie(u.Username, true);
                    }
                }
                catch (Exception e)
                { }
            }
            else
            {
                //accessToken not null
            }



        }

        [HttpPost]
        public void Logout(FormCollection form)
        {
            Session["accessToken"] = null;
            FormsAuthentication.SignOut();
        }

    }
}
