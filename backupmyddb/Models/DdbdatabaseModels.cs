namespace backupmyddb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Linq;

    public class DdbdatabaseModels : DbContext
    {
        // Your context has been configured to use a 'DdbdatabaseModels' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'backupmyddb.Models.DdbdatabaseModels' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DdbdatabaseModels' 
        // connection string in the application configuration file.
        public DdbdatabaseModels()
            : base("name=DdbdatabaseModels")
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

    public class Ddbdatabase
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Endpoint { get; set; }
        public string Authkey { get; set; }

        public virtual ICollection<Ddbcollection> Collections { get; set; }
    }

    public class CreateDatabaseViewModel
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }
        
        public string Endpoint { get; set; }
        public String AuthKey { get; set; }
    }
}