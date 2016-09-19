namespace backupmyddb.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DdbstoredprocedureModels : DbContext
    {
        // Your context has been configured to use a 'DdbstoredprocedureModels' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'backupmyddb.Models.DdbstoredprocedureModels' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DdbstoredprocedureModels' 
        // connection string in the application configuration file.
        public DdbstoredprocedureModels()
            : base("name=DdbstoredprocedureModels")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}

    public class Ddbstoredprocedure
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}