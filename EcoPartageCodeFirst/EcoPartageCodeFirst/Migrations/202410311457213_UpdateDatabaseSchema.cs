namespace EcoPartageCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDatabaseSchema : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "BlogId", "dbo.Blogs");
            DropIndex("dbo.Posts", new[] { "BlogId" });

            CreateTable(
            "dbo.Users",
            c => new
            {
                idUser = c.Int(nullable: false, identity: true),
                UserName = c.String(),
                Email = c.String(),
                Password = c.String(),
                Role = c.String(),
            })
            .PrimaryKey(t => t.idUser);

            CreateTable(
                "dbo.Annonces",
                c => new
                    {
                        idAnnonce = c.Int(nullable: false, identity: true),
                        Titre = c.String(),
                        Description = c.String(),
                        Points = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                        idUser = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idAnnonce)
                .ForeignKey("dbo.Users", t => t.idUser, cascadeDelete: true)
                .Index(t => t.idUser);
            

            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        idComment = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Notice = c.String(),
                        idUserGiven = c.Int(),
                        idUserRecipient = c.Int(),
                        Users_idUser = c.Int(),
                        Users_idUser1 = c.Int(),
                    })
                .PrimaryKey(t => t.idComment)
                .ForeignKey("dbo.Users", t => t.idUserGiven)
                .ForeignKey("dbo.Users", t => t.idUserRecipient)
                .ForeignKey("dbo.Users", t => t.Users_idUser)
                .ForeignKey("dbo.Users", t => t.Users_idUser1)
                .Index(t => t.idUserGiven)
                .Index(t => t.idUserRecipient)
                .Index(t => t.Users_idUser)
                .Index(t => t.Users_idUser1);
            
            DropTable("dbo.Blogs");
            DropTable("dbo.Posts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 128),
                        DisplayName = c.String(),
                    })
                .PrimaryKey(t => t.Username);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        BlogId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PostId);
            
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        BlogId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.BlogId);
            
            DropForeignKey("dbo.Annonces", "idUser", "dbo.Users");
            DropForeignKey("dbo.Comments", "Users_idUser1", "dbo.Users");
            DropForeignKey("dbo.Comments", "Users_idUser", "dbo.Users");
            DropForeignKey("dbo.Comments", "idUserRecipient", "dbo.Users");
            DropForeignKey("dbo.Comments", "idUserGiven", "dbo.Users");
            DropIndex("dbo.Comments", new[] { "Users_idUser1" });
            DropIndex("dbo.Comments", new[] { "Users_idUser" });
            DropIndex("dbo.Comments", new[] { "idUserRecipient" });
            DropIndex("dbo.Comments", new[] { "idUserGiven" });
            DropIndex("dbo.Annonces", new[] { "idUser" });
            DropTable("dbo.Comments");
            DropTable("dbo.Users");
            DropTable("dbo.Annonces");
            CreateIndex("dbo.Posts", "BlogId");
            AddForeignKey("dbo.Posts", "BlogId", "dbo.Blogs", "BlogId", cascadeDelete: true);
        }
    }
}
