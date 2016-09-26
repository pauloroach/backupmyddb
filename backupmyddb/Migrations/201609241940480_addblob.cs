namespace backupmyddb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addblob : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blob",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccountName = c.String(),
                        AccountKey = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CreateDatabaseViewModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Endpoint = c.String(),
                        AuthKey = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Ddbcollection",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.Int(nullable: false),
                        Ddbdatabase_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Ddbdatabase", t => t.Ddbdatabase_ID)
                .Index(t => t.Ddbdatabase_ID);
            
            CreateTable(
                "dbo.Ddbdatabase",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Endpoint = c.String(),
                        Authkey = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ddbcollection", "Ddbdatabase_ID", "dbo.Ddbdatabase");
            DropIndex("dbo.Ddbcollection", new[] { "Ddbdatabase_ID" });
            DropTable("dbo.Ddbdatabase");
            DropTable("dbo.Ddbcollection");
            DropTable("dbo.CreateDatabaseViewModel");
            DropTable("dbo.Blob");
        }
    }
}
