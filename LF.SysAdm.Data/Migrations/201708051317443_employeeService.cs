namespace LF.SysAdm.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class employeeService : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 80),
                        Function = c.Int(nullable: false),
                        Department = c.Int(nullable: false),
                        Document = c.String(nullable: false, maxLength: 16),
                        DateBirthday = c.DateTime(nullable: false),
                        DateRegister = c.DateTime(nullable: false),
                        DateOfChange = c.DateTime(),
                        RE = c.String(nullable: false, maxLength: 20),
                        AddressId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Address", t => t.AddressId)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.ServiceProvide",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Tempo = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateRegister = c.DateTime(nullable: false),
                        DateOfChanged = c.DateTime(),
                        Canceled = c.Boolean(nullable: false),
                        Description = c.String(maxLength: 400),
                        EmployeeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employee", t => t.EmployeeId)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceProvide", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.Employee", "AddressId", "dbo.Address");
            DropIndex("dbo.ServiceProvide", new[] { "EmployeeId" });
            DropIndex("dbo.Employee", new[] { "AddressId" });
            DropTable("dbo.ServiceProvide");
            DropTable("dbo.Employee");
        }
    }
}
