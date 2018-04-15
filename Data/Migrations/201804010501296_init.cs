namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PhotoProfiles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Url = c.String(),
                        HairColor = c.Int(nullable: false),
                        HairStyle = c.Int(nullable: false),
                        HairLength = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Birth = c.DateTime(nullable: false),
                        Gender = c.String(),
                        Sexuality = c.String(),
                        Description = c.String(),
                        CellphoneNumber = c.String(),
                        Email = c.String(),
                        SchoolLevel = c.String(),
                        Office = c.String(),
                        PhotoProfile_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PhotoProfiles", t => t.PhotoProfile_Id, cascadeDelete: true)
                .Index(t => t.PhotoProfile_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "PhotoProfile_Id", "dbo.PhotoProfiles");
            DropIndex("dbo.Users", new[] { "PhotoProfile_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.PhotoProfiles");
        }
    }
}
