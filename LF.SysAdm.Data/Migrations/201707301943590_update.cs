namespace LF.SysAdm.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Supply", "Phone", c => c.String(nullable: false, maxLength: 14, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Supply", "Phone", c => c.String(nullable: false, maxLength: 11, unicode: false));
        }
    }
}
