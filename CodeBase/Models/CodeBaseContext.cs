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
        public DbSet<Article> Articles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<File> Files { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           // modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public void SaveChanges()
        {
            base.SaveChanges();
        }
        
    }
}
