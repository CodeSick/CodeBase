using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace CodeBase.Models
{
    public interface ICodeBaseMembership
    {
        String LoggedInUser();
    }

    public class CodeBaseMembership : ICodeBaseMembership
    {
        public String LoggedInUser()
        {
            MembershipUser mu = Membership.GetUser();
            return mu.UserName;
        }
    }
}