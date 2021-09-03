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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.Address).IsRequired();
            builder.Property(x => x.City).IsRequired();
            builder.Property(x => x.Country).IsRequired();
            builder.Property(x => x.PostalCode).IsRequired();
            builder.Property(x => x.AccountNumber).IsRequired();
            builder.Property(x => x.Total).IsRequired();
            builder.Property(x => x.OrderStatus)
                   .HasDefaultValue(OrderStatus.Recieved);

            builder.HasMany(oi => oi.OrderItems).WithOne(o => o.Order).HasForeignKey(oi => oi.OrderId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
