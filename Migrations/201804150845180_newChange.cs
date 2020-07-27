namespace NabzeArz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CurrencyRates", "order_list", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CurrencyRates", "order_list");
        }
    }
}
