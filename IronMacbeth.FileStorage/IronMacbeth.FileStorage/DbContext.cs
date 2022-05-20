using IronMacbeth.FileStorage.Model;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace IronMacbeth.FileStorage
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        internal DbSet<File> Files { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<File>().ToTable(nameof(File));
        }
    }
}
