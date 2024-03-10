using Contact_zoo_at_home.Core.Entities.Comments;
using Contact_zoo_at_home.Core.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.CommentsAndNotificationsMapping
{
    internal class BaseCommentEntityConfiguration : IEntityTypeConfiguration<BaseComment>
    {
        public void Configure(EntityTypeBuilder<BaseComment> builder)
        {
            builder.UseTptMappingStrategy().ToTable(ConstantsForEFCore.TableNames.baseCommentsTableName);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Date)
                .IsRequired();

            builder.Property(x => x.Text)
                .HasMaxLength(ConstantsForEFCore.Sizes.commentMaxLength)
                .IsRequired();

            builder.HasOne(x => x.Author)
                .WithMany(x => x.MyComments)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
