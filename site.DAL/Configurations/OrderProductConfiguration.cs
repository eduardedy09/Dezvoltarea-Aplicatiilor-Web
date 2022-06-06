using site.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace site.DAL.Configurations
{
    class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Quantity)
                .HasColumnType("int");

            builder.HasOne(p => p.Order)
                .WithMany(p => p.OrderProducts)
                .HasForeignKey(p => p.OrderId);

            builder.HasOne(p => p.Product)
                .WithMany(p => p.OrderProducts)
                .HasForeignKey(p => p.ProductId);
        }
    }
}
