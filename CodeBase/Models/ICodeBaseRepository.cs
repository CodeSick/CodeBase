using System;
namespace CodeBase.Models
{
    public interface ICodeBaseRepository
    {
        System.Data.Entity.DbSet<Answer> Answers { get; set; }
        System.Data.Entity.DbSet<Article> Articles { get; set; }
        System.Data.Entity.DbSet<Category> Categories { get; set; }
        System.Data.Entity.DbSet<Comment> Comments { get; set; }
        System.Data.Entity.DbSet<File> Files { get; set; }
        System.Data.Entity.DbSet<Question> Questions { get; set; }
        System.Data.Entity.DbSet<Rating> Ratings { get; set; }
        System.Data.Entity.DbSet<User> Users { get; set; }
        void SaveChanges();
    }
}
