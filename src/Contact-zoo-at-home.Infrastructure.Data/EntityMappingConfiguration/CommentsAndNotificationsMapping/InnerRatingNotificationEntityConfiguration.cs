using Contact_zoo_at_home.Core.Entities.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.CommentsAndNotificationsMapping
{
    internal class InnerRatingNotificationEntityConfiguration : IEntityTypeConfiguration<InnerRatingNotification>
    {
        public void Configure(EntityTypeBuilder<InnerRatingNotification> builder)
        {
            builder.HasBaseType(typeof(InnerNotification));

            builder.HasOne(x => x.RateTargetUser)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.RateTargetPet)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
