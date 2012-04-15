using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
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
                dynamic fbuser = c.Get("me");
                int fbuserid = Convert.ToInt32(fbuser.id);

                User u = context.Users.SingleOrDefault(x => x.FbId == fbuserid);

                if (u == null)
                {
                    User newfbuser = new User()
                    {
                        Username = fbuser.name + " " + fbuser.surname,
                        FbId = fbuserid,
                        JoinDate = DateTime.Now
                    };
                    context.Users.Add(newfbuser);
                    context.SaveChanges();
                }
            }
        }

        [HttpPost]
        public void Logout(FormCollection form)
        {
            Session["accessToken"] = null;
        }

    }
}
