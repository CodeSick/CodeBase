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
                {}
                    
                u = context.Users.SingleOrDefault(x => x.FbId == fbuserid);

                if (u == null)
                {
                    Session["fbid"] = fbuserid;
                    return "null";
                }
                else if (u != null && fbuserid != -1)
                {
                    FormsAuthentication.SetAuthCookie(u.Username, true);
                    Session["fbuserchosenname"] = u.Username;
                    return u.Username;
                }
            }
            String localname = Session["fbuserchosenname"] as String;
            if (localname != null)
            {
                return localname;
            }
            return "";
        }

        [HttpPost]
        public void Logout(FormCollection form)
        {
            Session["accessToken"] = null;
            Session["fbid"] = null;
            Session["fbuserchosenname"] = null;
            FormsAuthentication.SignOut();
        }

    }
}
