namespace LF.SysAdm.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderEOrderItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        ChangeDate = c.DateTime(),
                        Status = c.Int(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Comments = c.String(maxLength: 400),
                        PaymentMethod = c.Int(nullable: false),
                        Discount = c.Decimal(precision: 18, scale: 2),
                        CustomerId = c.Guid(nullable: false),
                        EmployeeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customer", t => t.CustomerId)
                .ForeignKey("dbo.Employee", t => t.EmployeeId)
                .Index(t => t.CustomerId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.OrderItem",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ItemName = c.String(nullable: false, maxLength: 100),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        ProdId = c.Guid(nullable: false),
                        OrderId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Order", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
            AddColumn("dbo.ServiceProvide", "ServiceName", c => c.String(nullable: false, maxLength: 80));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.Order", "CustomerId", "dbo.Customer");
            DropForeignKey("dbo.OrderItem", "OrderId", "dbo.Order");
            DropIndex("dbo.OrderItem", new[] { "OrderId" });
            DropIndex("dbo.Order", new[] { "EmployeeId" });
            DropIndex("dbo.Order", new[] { "CustomerId" });
            DropColumn("dbo.ServiceProvide", "ServiceName");
            DropTable("dbo.OrderItem");
            DropTable("dbo.Order");
        }
    }
}
