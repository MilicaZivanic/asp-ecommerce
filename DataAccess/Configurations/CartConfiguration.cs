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
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.Property(x => x.Total).IsRequired();

            builder.HasMany(ci => ci.CartItems).WithOne(c => c.Cart).HasForeignKey(ci => ci.CartId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
