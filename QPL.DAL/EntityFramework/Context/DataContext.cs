using Microsoft.EntityFrameworkCore;
using QPL.DAL.Entities.Concrete;
using QPL.DAL.EntityFramework.Configurations;

namespace QPL.DAL.EntityFramework.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> dbOptions) : base(dbOptions)
        {

        }

        public DbSet<ProductDefinition> ProductDefinitions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductDefinitionConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ManufacturerConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
