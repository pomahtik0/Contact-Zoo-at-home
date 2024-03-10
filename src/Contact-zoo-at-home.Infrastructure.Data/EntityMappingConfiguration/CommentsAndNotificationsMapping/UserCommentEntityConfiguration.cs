using Contact_zoo_at_home.Core.Entities.Comments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.CommentsAndNotificationsMapping
{
    internal class UserCommentEntityConfiguration : IEntityTypeConfiguration<UserComment>
    {
        public void Configure(EntityTypeBuilder<UserComment> builder)
        {
            builder.HasBaseType(typeof(BaseComment));

            builder.HasOne(x => x.CommentTarget)
                .WithMany(x => x.Comments)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
