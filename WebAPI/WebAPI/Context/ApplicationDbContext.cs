using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebAPI.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace WebAPI.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("ApplicationDbContext")
        {
        }
        public DbSet<User> users { get; set; }
        public DbSet<Batch> batches { get; set; }
        public DbSet<Transaction> transactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) // Fluent API ??
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

}