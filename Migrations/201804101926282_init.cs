namespace NabzeArz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Channel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        last_channel_message = c.Int(nullable: false),
                        user_name = c.String(),
                        chat_id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CryptoRates",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        en_name = c.String(),
                        fa_name = c.String(),
                        symbol = c.String(),
                        rank = c.String(),
                        price_usd = c.String(),
                        price_btc = c.String(),
                        price_toman = c.String(),
                        price_rial = c.String(),
                        _24h_volume_usd = c.String(),
                        market_cap_usd = c.String(),
                        available_supply = c.String(),
                        total_supply = c.String(),
                        max_supply = c.String(),
                        percent_change_1h = c.String(),
                        percent_change_24h = c.String(),
                        percent_change_7d = c.String(),
                        persianDate = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Currency",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        symbol = c.String(),
                        Name = c.String(),
                        persianName = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.possension",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        user_id = c.Int(nullable: false),
                        currency_id = c.Int(nullable: false),
                        amount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Currency", t => t.currency_id, cascadeDelete: true)
                .ForeignKey("dbo.UserBot", t => t.user_id, cascadeDelete: true)
                .Index(t => t.user_id)
                .Index(t => t.currency_id);
            
            CreateTable(
                "dbo.UserBot",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        userName = c.String(),
                        chatId = c.Long(nullable: false),
                        name = c.String(),
                        family = c.String(),
                        operation = c.String(),
                        affiliate_id = c.String(),
                        walletDogcoin = c.String(),
                        refer_chat_id = c.Long(nullable: false),
                        point = c.Int(nullable: false),
                        last_activity = c.DateTime(nullable: false),
                        is_join_channel = c.Boolean(nullable: false),
                        verification_code = c.Int(nullable: false),
                        isCompleteRegister = c.Boolean(nullable: false),
                        phone = c.String(),
                        unsubscriber = c.Boolean(nullable: false),
                        email = c.String(),
                        language = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Point",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false),
                        Amount = c.Int(nullable: false),
                        Dont_count = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserBot", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.ticketModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        user_id = c.Int(nullable: false),
                        date = c.DateTime(nullable: false),
                        title = c.String(),
                        description = c.String(),
                        status = c.String(),
                        type = c.String(),
                        isDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.UserBot", t => t.user_id, cascadeDelete: true)
                .Index(t => t.user_id);
            
            CreateTable(
                "dbo.CurrencyRates",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        symbol = c.String(),
                        min = c.String(),
                        max = c.String(),
                        current = c.String(),
                        changePercent = c.String(),
                        changePrice = c.Int(nullable: false),
                        showIntoChannel = c.Boolean(nullable: false),
                        fa_name = c.String(),
                        en_name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.GoldRates",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        symbol = c.String(),
                        min = c.String(),
                        max = c.String(),
                        current = c.String(),
                        lastUpdate = c.DateTime(nullable: false),
                        changePercent = c.String(),
                        changePrice = c.Int(nullable: false),
                        fa_name = c.String(),
                        en_name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
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
                "dbo.UserTracking",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        chatId = c.Long(nullable: false),
                        operation = c.String(),
                        date = c.DateTime(nullable: false),
                        requestType = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.possension", "user_id", "dbo.UserBot");
            DropForeignKey("dbo.ticketModels", "user_id", "dbo.UserBot");
            DropForeignKey("dbo.Point", "User_Id", "dbo.UserBot");
            DropForeignKey("dbo.possension", "currency_id", "dbo.Currency");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ticketModels", new[] { "user_id" });
            DropIndex("dbo.Point", new[] { "User_Id" });
            DropIndex("dbo.possension", new[] { "currency_id" });
            DropIndex("dbo.possension", new[] { "user_id" });
            DropTable("dbo.UserTracking");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.GoldRates");
            DropTable("dbo.CurrencyRates");
            DropTable("dbo.ticketModels");
            DropTable("dbo.Point");
            DropTable("dbo.UserBot");
            DropTable("dbo.possension");
            DropTable("dbo.Currency");
            DropTable("dbo.CryptoRates");
            DropTable("dbo.Channel");
        }
    }
}
