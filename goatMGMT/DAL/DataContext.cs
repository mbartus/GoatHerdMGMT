using goatMGMT.Models;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.SqlServer;

namespace goatMGMT.DAL
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DataContext")
        {
        }

        public DbSet<Animals> Animals { get; set; }
        public DbSet<Children> Children { get; set; }
        public DbSet<Breedings> Breedings { get; set; }
        public DbSet<Births> Births { get; set; }
        public DbSet<Treatments> Treatments { get; set; }
        public DbSet<AnimalTreatments> AnimalTreatments { get; set; }
        public DbSet<Associates> Associates { get; set; }
        public DbSet<Transactions> Transactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }   
}