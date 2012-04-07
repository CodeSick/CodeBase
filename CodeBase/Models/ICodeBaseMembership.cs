using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeBase.Models
{
    public interface ICodeBaseMembership
    {
        String LoggedInUser();
    }
}