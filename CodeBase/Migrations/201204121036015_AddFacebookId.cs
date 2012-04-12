namespace CodeBase.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddFacebookId : DbMigration
    {
        public override void Up()
        {
            AddColumn("Users", "FbId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Users", "FbId");
        }
    }
}
