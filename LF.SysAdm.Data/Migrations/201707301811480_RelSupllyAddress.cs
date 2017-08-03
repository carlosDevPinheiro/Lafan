namespace LF.SysAdm.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class RelSupllyAddress : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Street = c.String(nullable: false, maxLength: 80, unicode: false),
                        Number = c.Int(nullable: false),
                        Complement = c.String(nullable: false, maxLength: 80, unicode: false),
                        District = c.String(nullable: false, maxLength: 50, unicode: false),
                        City = c.String(nullable: false, maxLength: 50, unicode: false),
                        State = c.String(nullable: false, maxLength: 50, unicode: false),
                        CEP = c.String(nullable: false, maxLength: 9, unicode: false),
                        DateRegister = c.DateTime(nullable: false),
                        DateOfChange = c.DateTime(),
                        CustomerId = c.Guid(),
                        Rel_Supply_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Supply", t => t.Rel_Supply_ID)
                .Index(t => t.CustomerId)
                .Index(t => t.Rel_Supply_ID);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Document = c.String(nullable: false, maxLength: 16, unicode: false),
                        DateBirthday = c.DateTime(nullable: false),
                        Phone = c.String(nullable: false, maxLength: 14, unicode: false),
                        Gender = c.Boolean(nullable: false),
                        DateRegister = c.DateTime(nullable: false),
                        DateOfChange = c.DateTime(),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.Document, unique: true, name: "IX_Name")
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                        Email = c.String(nullable: false, maxLength: 100, unicode: false),
                        Password = c.String(nullable: false, maxLength: 32, unicode: false),
                        RegistrationDate = c.DateTime(nullable: false),
                        DateofChange = c.DateTime(),
                        Profile = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Email, unique: true, name: "IX_Name");
            
            CreateTable(
                "dbo.Supply",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CompanyName = c.String(nullable: false, maxLength: 50, unicode: false),
                        CNPJ = c.String(nullable: false, maxLength: 18, unicode: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "CNPJ_IX",
                                    new AnnotationValues(oldValue: null, newValue: "IndexAnnotation: { IsUnique: True }")
                                },
                            }),
                        Phone = c.String(nullable: false, maxLength: 11, unicode: false),
                        Agent = c.String(nullable: false, maxLength: 11, unicode: false),
                        Email = c.String(nullable: false, maxLength: 100, unicode: false),
                        DateRegister = c.DateTime(nullable: false),
                        DateOfChange = c.DateTime(),
                        AddressId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Address", t => t.AddressId)
                .Index(t => t.AddressId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Address", "Rel_Supply_ID", "dbo.Supply");
            DropForeignKey("dbo.Supply", "AddressId", "dbo.Address");
            DropForeignKey("dbo.Customer", "UserId", "dbo.Users");
            DropForeignKey("dbo.Address", "CustomerId", "dbo.Customer");
            DropIndex("dbo.Supply", new[] { "AddressId" });
            DropIndex("dbo.Users", "IX_Name");
            DropIndex("dbo.Customer", new[] { "UserId" });
            DropIndex("dbo.Customer", "IX_Name");
            DropIndex("dbo.Address", new[] { "Rel_Supply_ID" });
            DropIndex("dbo.Address", new[] { "CustomerId" });
            DropTable("dbo.Supply",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "CNPJ",
                        new Dictionary<string, object>
                        {
                            { "CNPJ_IX", "IndexAnnotation: { IsUnique: True }" },
                        }
                    },
                });
            DropTable("dbo.Users");
            DropTable("dbo.Customer");
            DropTable("dbo.Address");
        }
    }
}
