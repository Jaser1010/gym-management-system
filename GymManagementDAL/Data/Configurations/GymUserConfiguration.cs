using GymManagementDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.Configurations
{
    internal class GymUserConfiguration<T> : IEntityTypeConfiguration<T> where T : GymUser
    {

        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.Name)
                .HasColumnType("varchar")
                .HasMaxLength(50);

            builder.Property(x => x.Email)
                .HasColumnType("varchar")
                .HasMaxLength(100);

            builder.Property(x => x.Phone)
              .HasColumnType("varchar")
              .HasMaxLength(11);

            builder.ToTable(Tb =>
            {
                Tb.HasCheckConstraint("ymUserValidEmailCheck", "Email LIKE '_%@_%._%'");
                Tb.HasCheckConstraint("ymUserValidPhoneCheck", "Phone Like '01%' and Phone Not Like '%[^0-9]%'");

            });

            builder.HasIndex(x => x.Email)
                .IsUnique();
            builder.HasIndex(x => x.Phone)
                .IsUnique();

            builder.OwnsOne(x => x.Address, AddressBuilder =>
            {
                AddressBuilder.Property(a => a.BuildingNumber)
                    .HasColumnName("BuildingNumber");
                AddressBuilder.Property(a => a.Street)
                    .HasColumnName("Street")
                    .HasColumnType("varchar")
                    .HasMaxLength(30);
                AddressBuilder.Property(a => a.City)
                    .HasColumnName("City")
                    .HasColumnType("varchar")
                    .HasMaxLength(30);
            });
        }
    }
}
