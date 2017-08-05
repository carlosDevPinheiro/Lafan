namespace LF.SysAdm.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VoltarNameTabelaOrder : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Ordered", newName: "Order");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Order", newName: "Ordered");
        }
    }
}
