using backupmyddb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace backupmyddb.DAL
{
    
    public class DdbContext : DbContext
    {

        public DdbContext() : base("DefaulConnection")
        {

        }

        public DbSet<Ddbdatabase> Ddbdatabases { get; set; }
        public DbSet<Ddbcollection> Ddbcollections { get; set; }
        public DbSet<Blob> Blobs { get; set; }
        //public DbSet<Ddbstoredprocedure> Ddbstoredprocedures { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<DdbContext>(null);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<backupmyddb.Models.CreateDatabaseViewModel> CreateDatabaseViewModels { get; set; }

        //public System.Data.Entity.DbSet<backupmyddb.Models.Blob> Blobs { get; set; }
    }
}