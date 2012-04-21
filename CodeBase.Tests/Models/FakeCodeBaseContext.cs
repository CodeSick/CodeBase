using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using CodeBase.Models;

namespace CodeBase.Tests.Models
{

    public partial class FakeCodeBaseContext : CodeBaseContext
    {

        public override System.Data.Entity.IDbSet<Answer> Answers { get; set; }
        public override System.Data.Entity.IDbSet<Article> Articles { get; set; }
        public override System.Data.Entity.IDbSet<Category> Categories { get; set; }
        public override System.Data.Entity.IDbSet<Comment> Comments { get; set; }
        public override System.Data.Entity.IDbSet<File> Files { get; set; }
        public override System.Data.Entity.IDbSet<Question> Questions { get; set; }
        public override System.Data.Entity.IDbSet<Rating> Ratings { get; set; }
        public override System.Data.Entity.IDbSet<User> Users { get; set; }


        public override int  SaveChanges()
        {
            // do nothing (probably set a variable as saved for testing)
            return 0;
        }

        public FakeCodeBaseContext()
        {
            Articles = new FakeDbSet<Article>();
            Users = new FakeDbSet<User>();
            Categories = new FakeDbSet<Category>();

        }


    }
}
