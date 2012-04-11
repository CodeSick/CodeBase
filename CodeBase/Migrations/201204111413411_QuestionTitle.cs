namespace CodeBase.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class QuestionTitle : DbMigration
    {
        public override void Up()
        {
            AddColumn("Questions", "Title", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            DropColumn("Questions", "Title");
        }
    }
}
