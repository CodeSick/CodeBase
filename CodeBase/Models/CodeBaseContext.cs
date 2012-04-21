using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace CodeBase.Models
{
    public class CodeBaseContext : DbContext, CodeBase.Models.ICodeBaseRepository
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<CodeBase.Models.CodeBaseContext>());

        public virtual IDbSet<Article> Articles { get; set; }

        public virtual IDbSet<User> Users { get; set; }

        public virtual IDbSet<Category> Categories { get; set; }

        public virtual IDbSet<Answer> Answers { get; set; }

        public virtual IDbSet<Question> Questions { get; set; }

        public virtual IDbSet<Comment> Comments { get; set; }

        public virtual IDbSet<File> Files { get; set; }

        public virtual IDbSet<Rating> Ratings { get; set; }

        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           // modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
        
    }
}
