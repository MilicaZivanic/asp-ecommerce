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
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description).IsRequired(false);
            builder.Property(x => x.DiscountPercent).IsRequired();

            builder.HasMany(p => p.Products).WithOne(d => d.Discount).HasForeignKey(p => p.DiscountId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
