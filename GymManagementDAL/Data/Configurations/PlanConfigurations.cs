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
    internal class PlanConfigurations : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.Property(x => x.Name)
                .HasColumnType("varchar")
                .HasMaxLength(50);
            builder.Property(x => x.Description)
                .HasColumnType("varchar")
                .HasMaxLength(100);
            builder.Property(x => x.Price)
                .HasPrecision(10, 2);
            builder.ToTable(Tb =>             {
                Tb.HasCheckConstraint("PlanValidDurationCheck", "DurationDays Between 1 and 365 ");
            });
        }
    }
}
