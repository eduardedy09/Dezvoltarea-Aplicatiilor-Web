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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100)
                .IsRequired(true);

            builder.Property(x => x.Description)
                .HasColumnType("nvarchar(500)")
                .HasMaxLength(500)
                .IsRequired(true);

            builder.Property(x => x.Image)
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100)
                .IsRequired(true);

            builder.Property(x => x.Price)
                .HasColumnType("decimal(8,2)")
                .IsRequired(true);

            builder.HasOne(p => p.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.CategoryId);
        }
    }
}
