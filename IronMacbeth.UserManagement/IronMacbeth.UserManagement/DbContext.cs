using IronMacbeth.UserManagement.Model;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace IronMacbeth.UserManagement
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        internal DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable(nameof(User))
                .HasKey(nameof(User.Login));

            modelBuilder.Entity<User>()
                .Property(x => x.UserRole).HasColumnName("RoleId");
        }
    }
}
