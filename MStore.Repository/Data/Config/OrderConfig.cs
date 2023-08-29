using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MStore.Core.Entities.OrderAgregate;

namespace MStore.Repository.Data.Config
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(O => O.ShippingAddress, NP => NP.WithOwner());
            builder.Property(O => O.Status)
                .HasConversion(
                    OStatus => OStatus.ToString(),
                    OStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), OStatus)
                    );
            builder.HasMany(O => O.Items).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.Property(O => O.SubTotal).HasColumnType("decimal(18,2)");
        }
    }
}
