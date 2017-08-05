namespace LF.SysAdm.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterarNameTabelaOrder : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Order", newName: "Ordered");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Ordered", newName: "Order");
        }
    }
}
