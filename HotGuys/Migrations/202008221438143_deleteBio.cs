namespace HotGuys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteBio : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HotChoiceViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Single(nullable: false),
                        UserId = c.String(maxLength: 128),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            DropColumn("dbo.AspNetUsers", "Bio");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Bio", c => c.String());
            DropForeignKey("dbo.HotChoiceViewModels", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.HotChoiceViewModels", new[] { "UserId" });
            DropTable("dbo.HotChoiceViewModels");
        }
    }
}
