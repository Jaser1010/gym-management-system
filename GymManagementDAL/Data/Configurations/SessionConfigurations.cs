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
    internal class SessionConfigurations : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable(x => 
            {
                x.HasCheckConstraint("SessionValidCapacityCheck", "Capacity Between 1 and 25");
                x.HasCheckConstraint("SessionValidStartEndDateCheck", "EndDate > StartDate");
            });

            builder.HasOne(x => x.SessionCategory)
                .WithMany(c => c.Sessions)
                .HasForeignKey(x => x.CategoryId);

            builder.HasOne(x => x.SessionTrainer)
                .WithMany(t => t.TrainerSessions)
                .HasForeignKey(x => x.TrainerId);
        }
        

        

    }
}
