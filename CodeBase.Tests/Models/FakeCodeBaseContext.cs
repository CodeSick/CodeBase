using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using CodeBase.Models;

namespace CodeBase.Tests.Models
{

    public partial class FakeCodeBaseContext : ICodeBaseRepository
    {

        public System.Data.Entity.IDbSet<Answer> Answers { get; set; }
        public System.Data.Entity.IDbSet<Article> Articles { get; set; }
        public System.Data.Entity.IDbSet<Category> Categories { get; set; }
        public System.Data.Entity.IDbSet<Comment> Comments { get; set; }
        public System.Data.Entity.IDbSet<File> Files { get; set; }
        public System.Data.Entity.IDbSet<Question> Questions { get; set; }
        public System.Data.Entity.IDbSet<Rating> Ratings { get; set; }
        public System.Data.Entity.IDbSet<User> Users { get; set; }

        public IDbSet<T> Set<T>() where T : class
        {
            foreach (PropertyInfo property in typeof(FakeCodeBaseContext).GetProperties())
            {
                if (property.PropertyType == typeof(IDbSet<T>))
                    return property.GetValue(this, null) as IDbSet<T>;
            }
            throw new Exception("Type collection not found");
        }

        public void SaveChanges()
        {
            // do nothing (probably set a variable as saved for testing)
        }

        public FakeCodeBaseContext()
        {
            Articles = new FakeDbSet<Article>();

        }


    }
}
