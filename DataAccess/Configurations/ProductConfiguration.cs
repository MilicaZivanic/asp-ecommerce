using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDataAccess.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();

            builder.HasMany(oi => oi.OrderItems).WithOne(p => p.Product).HasForeignKey(oi => oi.ProductId).OnDelete(DeleteBehavior.SetNull);
            builder.HasMany(ci => ci.CartItems).WithOne(p => p.Product).HasForeignKey(ci => ci.ProductId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
