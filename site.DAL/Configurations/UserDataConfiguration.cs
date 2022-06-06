using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using site.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace site.DAL.Configurations
{
    public class UserDataConfiguration : IEntityTypeConfiguration<UserData>
    {
        public void Configure(EntityTypeBuilder<UserData> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired(true);

            builder.Property(x => x.LastName)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired(true);

            builder.Property(x => x.Phone)
                .HasColumnType("nvarchar(15)")
                .HasMaxLength(15)
                .IsRequired(true);

            builder.Property(x => x.Address)
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(x => x.City)
                .HasColumnType("nvarchar(30)")
                .HasMaxLength(30);

            builder.Property(x => x.County)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(x => x.Country)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(x => x.Zipcode)
                .HasColumnType("nvarchar(10)")
                .HasMaxLength(10);

            builder.HasOne(p => p.User)
                .WithOne(p => p.UserData)
                .HasForeignKey<UserData>(p => p.UserId);
        }
    }
}
