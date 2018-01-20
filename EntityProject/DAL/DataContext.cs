using EntityProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace EntityProject.DAL
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DataContext")
        {

        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<PositionInQueue> PositionInQueues { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<DataContext>(new CreateDatabaseIfNotExists<DataContext>());
            Database.SetInitializer<DataContext>(new DropCreateDatabaseIfModelChanges<DataContext>());
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }


    }
}