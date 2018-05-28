using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using ProductCatalog.Model;

namespace ProductCatalog.DataAccess.EntityFramework
{
    public class ProductDBContext: DbContext
    {
        public ProductDBContext(DbContextOptions<ProductDBContext> options) : base(options)
        {
            if (!options.Extensions.OfType<InMemoryOptionsExtension>().Any())
            {
                Database.Migrate();
            }
        }

        public DbSet<ProductModel> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Entity<ProductModal>().ToTable("DirectDebitDetails")
                .HasIndex(u => u.ApplicationId)
                .IsUnique();

            */
        }
    }
}

