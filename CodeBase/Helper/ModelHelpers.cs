using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using CodeBase.Models;

namespace CodeBase.Helper
{
    public class ModelHelpers
    {
        //Roles allowed to edit
        static private String[] editor = new String[] { "Admin", "Editor" };

        //Roles allowed to acces admin stuff
        static private String[] admin = new String[] { "Admin" };



        static CodeBaseMembership m = new CodeBaseMembership();
        public static Boolean canEdit(Comment c)
        {
            var user = m.LoggedInUser();
            if (user != null && (user == c.Author.Username || Roles.GetRolesForUser().Intersect(editor).Count() > 0))
            {
                return true;
            }
            return false;
        }

        public static bool isEditor()
        {
            var user = m.LoggedInUser();
            if (user != null && (Roles.GetRolesForUser().Intersect(editor).Count() > 0))
            {
                return true;
            }
            return false;
        }

        public static Boolean canEdit(Article c)
        {
            var user = m.LoggedInUser();
            if (user != null && (user == c.Author.Username || Roles.GetRolesForUser().Intersect(editor).Count() > 0))
            {
                return true;
            }
            return false;
        }

        public static String createFilePath(Models.File f)
        {
            return "article" + f.FileId + "_" + f.Filename;
        }
    }
}