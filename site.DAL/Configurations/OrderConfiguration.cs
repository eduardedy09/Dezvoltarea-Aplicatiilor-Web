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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Date)
                .HasColumnType("datetime")
                .HasDefaultValue(DateTime.Now);

            builder.Property(x => x.Status)
                .HasColumnType("nvarchar(20)")
                .HasMaxLength(20);
        }
    }
}
