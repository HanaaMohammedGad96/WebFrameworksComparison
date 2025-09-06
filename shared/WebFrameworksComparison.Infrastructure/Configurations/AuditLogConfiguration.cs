using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharpCompress.Common;

namespace WebFrameworksComparison.Infrastructure.Configurations
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Action)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.EntityName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.CreatedAt)
                .IsRequired();

            builder.HasOne(e => e.User)
                  .WithMany(e => e.AuditLogs)
                  .HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
