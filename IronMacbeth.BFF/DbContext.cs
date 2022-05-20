using IronMacbeth.BFF.Contract;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace IronMacbeth.BFF
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        internal DbSet<Order> Orders { get; set; }

        public DbSet<Periodical> Periodicals { get; set; }

        public DbSet<Thesis> Thesises { get; set; }

        public DbSet<Newspaper> Newspapers { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Order>()
             .ToTable(nameof(Order));

            modelBuilder.Entity<Article>()
              .ToTable(nameof(Article));

            modelBuilder.Entity<Book>()
                .ToTable(nameof(Book));

            modelBuilder.Entity<Newspaper>()
              .ToTable(nameof(Newspaper));

            modelBuilder.Entity<Thesis>()
             .ToTable(nameof(Thesis));

            modelBuilder.Entity<Periodical>()
            .ToTable(nameof(Periodical));
        }
    }
}
