using System;
namespace CodeBase.Models
{
    public interface ICodeBaseRepository
    {
        System.Data.Entity.IDbSet<Answer> Answers { get; set; }
        System.Data.Entity.IDbSet<Article> Articles { get; set; }
        System.Data.Entity.IDbSet<Category> Categories { get; set; }
        System.Data.Entity.IDbSet<Comment> Comments { get; set; }
        System.Data.Entity.IDbSet<File> Files { get; set; }
        System.Data.Entity.IDbSet<Question> Questions { get; set; }
        System.Data.Entity.IDbSet<Rating> Ratings { get; set; }
        System.Data.Entity.IDbSet<User> Users { get; set; }
    }
}
