using ClothingStoreFranchise.NetCore.Catalog.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;

namespace ClothingStoreFranchise.NetCore.Catalog
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {
        }

        public DbSet<CatalogProduct> CatalogProducts { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Offer> Offers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             modelBuilder.Entity<Category>()
                .Property(x => x.Id)
                .UseIdentityColumn(seed: 0, increment: 1);
        }
    }

    public class CatalogContextFactory : IDesignTimeDbContextFactory<CatalogContext>
    {
        public CatalogContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CatalogContext>()
                .UseSqlServer(@"data source=127.0.0.1; initial catalog=Catalog; persist security info=True; user id=sqlserver; password=root")
                .EnableSensitiveDataLogging(true)
                .UseLoggerFactory(new LoggerFactory());

            return new CatalogContext(optionsBuilder.Options);
        }
    }
}
