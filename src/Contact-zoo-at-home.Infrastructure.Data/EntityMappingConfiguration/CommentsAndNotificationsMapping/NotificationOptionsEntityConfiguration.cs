using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Contact_zoo_at_home.Core.Entities.Notifications;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.CommentsAndNotificationsMapping
{
    internal class NotificationOptionsEntityConfiguration : IEntityTypeConfiguration<NotificationOptions>
    {
        public void Configure(EntityTypeBuilder<NotificationOptions> builder)
        {
            builder.HasKey(x => x.TargetId);
        }
    }
}
