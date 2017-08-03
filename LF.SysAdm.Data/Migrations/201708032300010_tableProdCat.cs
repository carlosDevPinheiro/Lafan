namespace LF.SysAdm.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tableProdCat : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        NameCategory = c.String(nullable: false, maxLength: 50, unicode: false),
                        DescriptionCategory = c.String(nullable: false, maxLength: 100, unicode: false),
                        DateRegister = c.DateTime(nullable: false),
                        DateOfChange = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 60, unicode: false),
                        Description = c.String(nullable: false, maxLength: 100, unicode: false),
                        DateExpiration = c.DateTime(),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateRegister = c.DateTime(nullable: false),
                        DateOfChange = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        Image = c.String(nullable: false, maxLength: 8000, unicode: false),
                        Batch = c.String(nullable: false, maxLength: 50, unicode: false),
                        Invoice = c.Int(nullable: false),
                        CategoryId = c.Guid(nullable: false),
                        SupplyId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Category", t => t.CategoryId)
                .ForeignKey("dbo.Supply", t => t.SupplyId)
                .Index(t => t.CategoryId)
                .Index(t => t.SupplyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "SupplyId", "dbo.Supply");
            DropForeignKey("dbo.Product", "CategoryId", "dbo.Category");
            DropIndex("dbo.Product", new[] { "SupplyId" });
            DropIndex("dbo.Product", new[] { "CategoryId" });
            DropTable("dbo.Product");
            DropTable("dbo.Category");
        }
    }
}
