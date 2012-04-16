namespace CodeBase.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class ArticlesAddApproved : DbMigration
    {
        public override void Up()
        {
            AddColumn("Articles", "Approved", c => c.Boolean(nullable: false, defaultValue: false));
            Sql("Update Articles set Approved = 'True'");
        }
        
        public override void Down()
        {
            DropColumn("Articles", "Approved");
        }
    }
}
