using Contact_zoo_at_home.Core.Entities.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.CommentsAndNotificationsMapping
{
    internal class InnerNotificationEntityConfiguration : IEntityTypeConfiguration<InnerNotification>
    {
        public void Configure(EntityTypeBuilder<InnerNotification> builder)
        {
            builder.UseTptMappingStrategy();

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Status).IsRequired();

            builder.Property(x => x.Title)
                .HasMaxLength(Sizes.ShortTitlesLength)
                .IsRequired();

            builder.Property(x => x.Text)
                .HasMaxLength(Sizes.CommentMaxLength)
                .IsRequired();

            builder.HasOne(x => x.NotificationTarget)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
