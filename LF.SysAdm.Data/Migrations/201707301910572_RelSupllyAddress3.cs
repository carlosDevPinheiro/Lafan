namespace LF.SysAdm.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelSupllyAddress3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Supply", "Agent", c => c.String(nullable: false, maxLength: 40, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Supply", "Agent", c => c.String(nullable: false, maxLength: 11, unicode: false));
        }
    }
}
