using System;
using System.Reflection;
using cleanArchitecture.Core.Entities.ProductAggregate;
using Microsoft.EntityFrameworkCore;

namespace cleanArchitecture.Infra.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products;

        public DbSet<ProductOption> ProductOptions;

        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        
    }
}
