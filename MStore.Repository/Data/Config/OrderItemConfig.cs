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
    public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(OI => OI.Product, NP => NP.WithOwner());
            builder.Property(OI => OI.Price).HasColumnType("decimal(18,2)");
        }
    }
}
