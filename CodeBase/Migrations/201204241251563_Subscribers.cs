namespace CodeBase.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Subscribers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "UserQuestions",
                c => new
                    {
                        User_UserId = c.Int(nullable: false),
                        Question_QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_UserId, t.Question_QuestionId })
                .ForeignKey("Users", t => t.User_UserId, cascadeDelete: true)
                .ForeignKey("Questions", t => t.Question_QuestionId, cascadeDelete: true)
                .Index(t => t.User_UserId)
                .Index(t => t.Question_QuestionId);
            
            CreateTable(
                "UserArticles",
                c => new
                    {
                        User_UserId = c.Int(nullable: false),
                        Article_ArticleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_UserId, t.Article_ArticleId })
                .ForeignKey("Users", t => t.User_UserId, cascadeDelete: true)
                .ForeignKey("Articles", t => t.Article_ArticleId, cascadeDelete: true)
                .Index(t => t.User_UserId)
                .Index(t => t.Article_ArticleId);
            
        }
        
        public override void Down()
        {
            DropIndex("UserArticles", new[] { "Article_ArticleId" });
            DropIndex("UserArticles", new[] { "User_UserId" });
            DropIndex("UserQuestions", new[] { "Question_QuestionId" });
            DropIndex("UserQuestions", new[] { "User_UserId" });
            DropForeignKey("UserArticles", "Article_ArticleId", "Articles");
            DropForeignKey("UserArticles", "User_UserId", "Users");
            DropForeignKey("UserQuestions", "Question_QuestionId", "Questions");
            DropForeignKey("UserQuestions", "User_UserId", "Users");
            DropTable("UserArticles");
            DropTable("UserQuestions");
        }
    }
}
