namespace My_pro.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update0 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blog_Comments",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Blogid = c.String(nullable: false),
                        User_id_Co = c.String(nullable: false),
                        User_img = c.String(nullable: false),
                        User_Name = c.String(nullable: false),
                        Comment = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Toptext = c.String(nullable: false),
                        Buttomtext = c.String(nullable: false),
                        UserId = c.String(nullable: false),
                        UserName = c.String(),
                        Date = c.DateTime(nullable: false),
                        Link1 = c.String(nullable: false),
                        Link2 = c.String(nullable: false),
                        Link3 = c.String(nullable: false),
                        URL = c.String(nullable: false),
                        img = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Chapters",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Date = c.DateTime(nullable: false),
                        Level_id = c.String(nullable: false),
                        Level_Name = c.String(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.QExams_Chapters",
                c => new
                    {
                        QExam_Id = c.String(nullable: false, maxLength: 128),
                        Chapter_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.QExam_Id, t.Chapter_Id })
                .ForeignKey("dbo.QExams", t => t.QExam_Id)
                .ForeignKey("dbo.Chapters", t => t.Chapter_Id)
                .Index(t => t.QExam_Id)
                .Index(t => t.Chapter_Id);
            
            CreateTable(
                "dbo.QExams",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        LevelId = c.String(nullable: false),
                        LevelName = c.String(nullable: false),
                        Question = c.String(nullable: false),
                        S1 = c.String(nullable: false),
                        S2 = c.String(nullable: false),
                        S3 = c.String(nullable: false),
                        S4 = c.String(nullable: false),
                        S = c.String(nullable: false),
                        Final = c.Boolean(nullable: false),
                        Chapter = c.Boolean(nullable: false),
                        MoreQ = c.Boolean(nullable: false),
                        NumChapter = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.QExams_Finals",
                c => new
                    {
                        QExam_Id = c.String(nullable: false, maxLength: 128),
                        Final_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.QExam_Id, t.Final_Id })
                .ForeignKey("dbo.Finals", t => t.Final_Id)
                .ForeignKey("dbo.QExams", t => t.QExam_Id)
                .Index(t => t.QExam_Id)
                .Index(t => t.Final_Id);
            
            CreateTable(
                "dbo.Finals",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false),
                        Marks = c.Int(nullable: false),
                        Level_id = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users_Finals",
                c => new
                    {
                        User_Id = c.String(nullable: false, maxLength: 128),
                        Level_Id = c.String(nullable: false, maxLength: 128),
                        Marks = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Levels_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Level_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.Levels", t => t.Levels_Id)
                .ForeignKey("dbo.Finals", t => t.Level_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Level_Id)
                .Index(t => t.Levels_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Picture = c.String(),
                        XP = c.Int(nullable: false),
                        Facebook_Link = c.String(),
                        Linkedin_Link = c.String(),
                        Link = c.String(),
                        Gender = c.String(),
                        About = c.String(),
                        Country = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        UserID1 = c.String(nullable: false, maxLength: 128),
                        UserID2 = c.String(nullable: false, maxLength: 128),
                        Sender = c.String(nullable: false),
                        Accept = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID1, t.UserID2 })
                .ForeignKey("dbo.AspNetUsers", t => t.UserID1)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID2)
                .Index(t => t.UserID1)
                .Index(t => t.UserID2);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Users_Chapters",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        User_Id = c.String(nullable: false),
                        Chapter = c.Int(nullable: false),
                        Level = c.String(nullable: false),
                        Marks = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Users_Id = c.String(maxLength: 128),
                        Chapters_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Users_Id)
                .ForeignKey("dbo.Chapters", t => t.Chapters_Id)
                .Index(t => t.Users_Id)
                .Index(t => t.Chapters_Id);
            
            CreateTable(
                "dbo.Users_Diplomacd",
                c => new
                    {
                        User_Id = c.String(nullable: false, maxLength: 128),
                        Diploma_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Diploma_Id })
                .ForeignKey("dbo.Diplomata", t => t.Diploma_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Diploma_Id);
            
            CreateTable(
                "dbo.Diplomata",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Level = c.String(nullable: false),
                        UserId = c.String(nullable: false),
                        UserName = c.String(nullable: false),
                        Marks = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users_Levels",
                c => new
                    {
                        User_Id = c.String(nullable: false, maxLength: 128),
                        Level_Id = c.String(nullable: false, maxLength: 128),
                        Time = c.DateTime(nullable: false),
                        Marks = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Level_Id })
                .ForeignKey("dbo.Levels", t => t.Level_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Level_Id);
            
            CreateTable(
                "dbo.Levels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        Pic = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        About = c.String(nullable: false),
                        IsFree = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Levels_Videos",
                c => new
                    {
                        Level_Id = c.String(nullable: false, maxLength: 128),
                        Video_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Level_Id, t.Video_Id })
                .ForeignKey("dbo.Videos", t => t.Video_Id)
                .ForeignKey("dbo.Levels", t => t.Level_Id)
                .Index(t => t.Level_Id)
                .Index(t => t.Video_Id);
            
            CreateTable(
                "dbo.Videos",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Lectrue_Name = c.String(nullable: false),
                        URL = c.String(nullable: false),
                        Level_id = c.String(nullable: false),
                        Level_Name = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.UploadFiles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        User_Id = c.String(nullable: false),
                        URL = c.String(nullable: false),
                        Img = c.String(nullable: false),
                        Time = c.DateTime(nullable: false),
                        Type = c.String(nullable: false),
                        Size = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Users_Chapters", "Chapters_Id", "dbo.Chapters");
            DropForeignKey("dbo.QExams_Chapters", "Chapter_Id", "dbo.Chapters");
            DropForeignKey("dbo.QExams_Finals", "QExam_Id", "dbo.QExams");
            DropForeignKey("dbo.Users_Finals", "Level_Id", "dbo.Finals");
            DropForeignKey("dbo.Users_Levels", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Users_Levels", "Level_Id", "dbo.Levels");
            DropForeignKey("dbo.Users_Finals", "Levels_Id", "dbo.Levels");
            DropForeignKey("dbo.Levels_Videos", "Level_Id", "dbo.Levels");
            DropForeignKey("dbo.Levels_Videos", "Video_Id", "dbo.Videos");
            DropForeignKey("dbo.Users_Finals", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Users_Diplomacd", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Users_Diplomacd", "Diploma_Id", "dbo.Diplomata");
            DropForeignKey("dbo.Users_Chapters", "Users_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "UserID2", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "UserID1", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.QExams_Finals", "Final_Id", "dbo.Finals");
            DropForeignKey("dbo.QExams_Chapters", "QExam_Id", "dbo.QExams");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Levels_Videos", new[] { "Video_Id" });
            DropIndex("dbo.Levels_Videos", new[] { "Level_Id" });
            DropIndex("dbo.Users_Levels", new[] { "Level_Id" });
            DropIndex("dbo.Users_Levels", new[] { "User_Id" });
            DropIndex("dbo.Users_Diplomacd", new[] { "Diploma_Id" });
            DropIndex("dbo.Users_Diplomacd", new[] { "User_Id" });
            DropIndex("dbo.Users_Chapters", new[] { "Chapters_Id" });
            DropIndex("dbo.Users_Chapters", new[] { "Users_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Friends", new[] { "UserID2" });
            DropIndex("dbo.Friends", new[] { "UserID1" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Users_Finals", new[] { "Levels_Id" });
            DropIndex("dbo.Users_Finals", new[] { "Level_Id" });
            DropIndex("dbo.Users_Finals", new[] { "User_Id" });
            DropIndex("dbo.QExams_Finals", new[] { "Final_Id" });
            DropIndex("dbo.QExams_Finals", new[] { "QExam_Id" });
            DropIndex("dbo.QExams_Chapters", new[] { "Chapter_Id" });
            DropIndex("dbo.QExams_Chapters", new[] { "QExam_Id" });
            DropTable("dbo.UploadFiles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Videos");
            DropTable("dbo.Levels_Videos");
            DropTable("dbo.Levels");
            DropTable("dbo.Users_Levels");
            DropTable("dbo.Diplomata");
            DropTable("dbo.Users_Diplomacd");
            DropTable("dbo.Users_Chapters");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Friends");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Users_Finals");
            DropTable("dbo.Finals");
            DropTable("dbo.QExams_Finals");
            DropTable("dbo.QExams");
            DropTable("dbo.QExams_Chapters");
            DropTable("dbo.Chapters");
            DropTable("dbo.Blogs");
            DropTable("dbo.Blog_Comments");
        }
    }
}
