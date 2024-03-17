using Contact_zoo_at_home.Core.Entities.Comments;
using Contact_zoo_at_home.Core.Entities.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                .HasMaxLength(ConstantsForEFCore.Sizes.ShortTitlesLength)
                .IsRequired();

            builder.Property(x => x.Text)
                .HasMaxLength(ConstantsForEFCore.Sizes.CommentMaxLength)
                .IsRequired();

            builder.HasOne(x => x.NotificationTarget)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
