namespace Egreeting.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initdb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        CategorySlug = c.String(maxLength: 100),
                        CategoryName = c.String(maxLength: 100),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        Draft = c.Boolean(),
                    })
                .PrimaryKey(t => t.CategoryID)
                .Index(t => t.CategorySlug, unique: true);
            
            CreateTable(
                "dbo.Ecards",
                c => new
                    {
                        EcardID = c.Int(nullable: false, identity: true),
                        EcardName = c.String(maxLength: 150),
                        EcardSlug = c.String(maxLength: 200),
                        EcardType = c.Int(nullable: false),
                        EcardUrl = c.String(maxLength: 150),
                        ThumbnailUrl = c.String(maxLength: 150),
                        Price = c.Double(nullable: false),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        Draft = c.Boolean(),
                        EgreetingUser_EgreetingUserID = c.Int(),
                    })
                .PrimaryKey(t => t.EcardID)
                .ForeignKey("dbo.EgreetingUsers", t => t.EgreetingUser_EgreetingUserID)
                .Index(t => t.EcardSlug, unique: true)
                .Index(t => t.EgreetingUser_EgreetingUserID);
            
            CreateTable(
                "dbo.EgreetingUsers",
                c => new
                    {
                        EgreetingUserID = c.Int(nullable: false, identity: true),
                        EgreetingUserSlug = c.String(maxLength: 100),
                        FirstName = c.String(maxLength: 50),
                        Email = c.String(maxLength: 100),
                        LastName = c.String(maxLength: 100),
                        Avatar = c.Binary(),
                        BirthDay = c.DateTime(),
                        CreditCardNumber = c.String(maxLength: 12),
                        CreditCardCVG = c.String(maxLength: 3),
                        PaymentDueDate = c.DateTime(),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        Draft = c.Boolean(),
                    })
                .PrimaryKey(t => t.EgreetingUserID);
            
            CreateTable(
                "dbo.EgreetingRoles",
                c => new
                    {
                        EgreetingRoleID = c.Int(nullable: false, identity: true),
                        EgreetingRoleName = c.String(maxLength: 50),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        Draft = c.Boolean(),
                    })
                .PrimaryKey(t => t.EgreetingRoleID)
                .Index(t => t.EgreetingRoleName, unique: true);
            
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        FeedbackID = c.Int(nullable: false, identity: true),
                        Subject = c.String(maxLength: 200),
                        Message = c.String(maxLength: 500),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        Draft = c.Boolean(),
                        Ecard_EcardID = c.Int(),
                        EgreetingUser_EgreetingUserID = c.Int(),
                    })
                .PrimaryKey(t => t.FeedbackID)
                .ForeignKey("dbo.Ecards", t => t.Ecard_EcardID)
                .ForeignKey("dbo.EgreetingUsers", t => t.EgreetingUser_EgreetingUserID)
                .Index(t => t.Ecard_EcardID)
                .Index(t => t.EgreetingUser_EgreetingUserID);
            
            CreateTable(
                "dbo.ScheduleSenders",
                c => new
                    {
                        ScheduleSenderID = c.Int(nullable: false, identity: true),
                        ScheduleTime = c.DateTime(),
                        ScheduleType = c.Int(nullable: false),
                        SenderName = c.String(maxLength: 100),
                        RecipientEmail = c.String(maxLength: 100),
                        SendSubject = c.String(maxLength: 100),
                        SendMessage = c.String(maxLength: 500),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        Draft = c.Boolean(),
                        Ecard_EcardID = c.Int(),
                        EgreetingUser_EgreetingUserID = c.Int(),
                    })
                .PrimaryKey(t => t.ScheduleSenderID)
                .ForeignKey("dbo.Ecards", t => t.Ecard_EcardID)
                .ForeignKey("dbo.EgreetingUsers", t => t.EgreetingUser_EgreetingUserID)
                .Index(t => t.Ecard_EcardID)
                .Index(t => t.EgreetingUser_EgreetingUserID);
            
            CreateTable(
                "dbo.Subcribers",
                c => new
                    {
                        SubcriberID = c.Int(nullable: false),
                        Email = c.String(maxLength: 100),
                        EgreetingUserID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        Draft = c.Boolean(),
                    })
                .PrimaryKey(t => t.SubcriberID)
                .ForeignKey("dbo.EgreetingUsers", t => t.SubcriberID)
                .Index(t => t.SubcriberID);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderDetailID = c.Int(nullable: false, identity: true),
                        SendStatus = c.Boolean(nullable: false),
                        SendTime = c.DateTime(),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        Draft = c.Boolean(),
                        Ecard_EcardID = c.Int(),
                        Order_OrderID = c.Int(),
                    })
                .PrimaryKey(t => t.OrderDetailID)
                .ForeignKey("dbo.Ecards", t => t.Ecard_EcardID)
                .ForeignKey("dbo.Orders", t => t.Order_OrderID)
                .Index(t => t.Ecard_EcardID)
                .Index(t => t.Order_OrderID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        SenderName = c.String(maxLength: 100),
                        RecipientEmail = c.String(maxLength: 100),
                        SendSubject = c.String(maxLength: 100),
                        SendMessage = c.String(maxLength: 500),
                        ScheduleTime = c.DateTime(),
                        SendStatus = c.Boolean(nullable: false),
                        TotalPrice = c.Double(nullable: false),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        Draft = c.Boolean(),
                        User_EgreetingUserID = c.Int(),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.EgreetingUsers", t => t.User_EgreetingUserID)
                .Index(t => t.User_EgreetingUserID);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        PaymentID = c.Int(nullable: false, identity: true),
                        Month = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        PaymentStatus = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        Draft = c.Boolean(),
                        EgreetingUser_EgreetingUserID = c.Int(),
                    })
                .PrimaryKey(t => t.PaymentID)
                .ForeignKey("dbo.EgreetingUsers", t => t.EgreetingUser_EgreetingUserID)
                .Index(t => t.EgreetingUser_EgreetingUserID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        EgreetingRole_EgreetingRoleID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EgreetingRoles", t => t.EgreetingRole_EgreetingRoleID)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex")
                .Index(t => t.EgreetingRole_EgreetingRoleID);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
                        EgreetingUser_EgreetingUserID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EgreetingUsers", t => t.EgreetingUser_EgreetingUserID)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.EgreetingUser_EgreetingUserID);
            
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
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.EcardCategories",
                c => new
                    {
                        Ecard_EcardID = c.Int(nullable: false),
                        Category_CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Ecard_EcardID, t.Category_CategoryID })
                .ForeignKey("dbo.Ecards", t => t.Ecard_EcardID, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryID, cascadeDelete: true)
                .Index(t => t.Ecard_EcardID)
                .Index(t => t.Category_CategoryID);
            
            CreateTable(
                "dbo.EgreetingRoleEgreetingUsers",
                c => new
                    {
                        EgreetingRole_EgreetingRoleID = c.Int(nullable: false),
                        EgreetingUser_EgreetingUserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EgreetingRole_EgreetingRoleID, t.EgreetingUser_EgreetingUserID })
                .ForeignKey("dbo.EgreetingRoles", t => t.EgreetingRole_EgreetingRoleID, cascadeDelete: true)
                .ForeignKey("dbo.EgreetingUsers", t => t.EgreetingUser_EgreetingUserID, cascadeDelete: true)
                .Index(t => t.EgreetingRole_EgreetingRoleID)
                .Index(t => t.EgreetingUser_EgreetingUserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "EgreetingUser_EgreetingUserID", "dbo.EgreetingUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetRoles", "EgreetingRole_EgreetingRoleID", "dbo.EgreetingRoles");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Payments", "EgreetingUser_EgreetingUserID", "dbo.EgreetingUsers");
            DropForeignKey("dbo.Orders", "User_EgreetingUserID", "dbo.EgreetingUsers");
            DropForeignKey("dbo.OrderDetails", "Order_OrderID", "dbo.Orders");
            DropForeignKey("dbo.OrderDetails", "Ecard_EcardID", "dbo.Ecards");
            DropForeignKey("dbo.Subcribers", "SubcriberID", "dbo.EgreetingUsers");
            DropForeignKey("dbo.ScheduleSenders", "EgreetingUser_EgreetingUserID", "dbo.EgreetingUsers");
            DropForeignKey("dbo.ScheduleSenders", "Ecard_EcardID", "dbo.Ecards");
            DropForeignKey("dbo.Feedbacks", "EgreetingUser_EgreetingUserID", "dbo.EgreetingUsers");
            DropForeignKey("dbo.Feedbacks", "Ecard_EcardID", "dbo.Ecards");
            DropForeignKey("dbo.EgreetingRoleEgreetingUsers", "EgreetingUser_EgreetingUserID", "dbo.EgreetingUsers");
            DropForeignKey("dbo.EgreetingRoleEgreetingUsers", "EgreetingRole_EgreetingRoleID", "dbo.EgreetingRoles");
            DropForeignKey("dbo.Ecards", "EgreetingUser_EgreetingUserID", "dbo.EgreetingUsers");
            DropForeignKey("dbo.EcardCategories", "Category_CategoryID", "dbo.Categories");
            DropForeignKey("dbo.EcardCategories", "Ecard_EcardID", "dbo.Ecards");
            DropIndex("dbo.EgreetingRoleEgreetingUsers", new[] { "EgreetingUser_EgreetingUserID" });
            DropIndex("dbo.EgreetingRoleEgreetingUsers", new[] { "EgreetingRole_EgreetingRoleID" });
            DropIndex("dbo.EcardCategories", new[] { "Category_CategoryID" });
            DropIndex("dbo.EcardCategories", new[] { "Ecard_EcardID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "EgreetingUser_EgreetingUserID" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", new[] { "EgreetingRole_EgreetingRoleID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Payments", new[] { "EgreetingUser_EgreetingUserID" });
            DropIndex("dbo.Orders", new[] { "User_EgreetingUserID" });
            DropIndex("dbo.OrderDetails", new[] { "Order_OrderID" });
            DropIndex("dbo.OrderDetails", new[] { "Ecard_EcardID" });
            DropIndex("dbo.Subcribers", new[] { "SubcriberID" });
            DropIndex("dbo.ScheduleSenders", new[] { "EgreetingUser_EgreetingUserID" });
            DropIndex("dbo.ScheduleSenders", new[] { "Ecard_EcardID" });
            DropIndex("dbo.Feedbacks", new[] { "EgreetingUser_EgreetingUserID" });
            DropIndex("dbo.Feedbacks", new[] { "Ecard_EcardID" });
            DropIndex("dbo.EgreetingRoles", new[] { "EgreetingRoleName" });
            DropIndex("dbo.Ecards", new[] { "EgreetingUser_EgreetingUserID" });
            DropIndex("dbo.Ecards", new[] { "EcardSlug" });
            DropIndex("dbo.Categories", new[] { "CategorySlug" });
            DropTable("dbo.EgreetingRoleEgreetingUsers");
            DropTable("dbo.EcardCategories");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Payments");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Subcribers");
            DropTable("dbo.ScheduleSenders");
            DropTable("dbo.Feedbacks");
            DropTable("dbo.EgreetingRoles");
            DropTable("dbo.EgreetingUsers");
            DropTable("dbo.Ecards");
            DropTable("dbo.Categories");
        }
    }
}
