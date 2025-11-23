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
    internal class MemberSessionConfiguration : IEntityTypeConfiguration<MemberSession>
    {
        public void Configure(EntityTypeBuilder<MemberSession> builder)
        {
            builder.Ignore(x => x.Id);
            builder.HasKey(x => new { x.SessionId, x.MemberId });
            builder.Property(x => x.CreatedAt)
                .HasColumnName("bookingDate")
                .HasDefaultValueSql("GETDATE()");

			builder.HasOne(X => X.Session)
				   .WithMany(X => X.SessionMembers)
				   .HasForeignKey(X => X.SessionId);

			builder.HasOne(X => X.Member)
				   .WithMany(X => X.MemberSessions)
				   .HasForeignKey(X => X.MemberId);
		}
    }
}
