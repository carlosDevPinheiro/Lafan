namespace LF.SysAdm.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelSupllyAddress2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Address", "Rel_Supply_ID", "dbo.Supply");
            DropIndex("dbo.Address", new[] { "Rel_Supply_ID" });
            DropColumn("dbo.Address", "Rel_Supply_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Address", "Rel_Supply_ID", c => c.Guid());
            CreateIndex("dbo.Address", "Rel_Supply_ID");
            AddForeignKey("dbo.Address", "Rel_Supply_ID", "dbo.Supply", "ID");
        }
    }
}
