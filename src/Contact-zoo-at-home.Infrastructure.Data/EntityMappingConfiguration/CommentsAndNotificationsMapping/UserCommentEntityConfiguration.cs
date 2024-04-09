using Contact_zoo_at_home.Core.Entities.Comments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.CommentsAndNotificationsMapping
{
    internal class UserCommentEntityConfiguration : IEntityTypeConfiguration<UserComment>
    {
        public void Configure(EntityTypeBuilder<UserComment> builder)
        {
            builder.HasBaseType(typeof(BaseComment));

            builder.Property(x => x.CommentRating)
                .HasColumnType(Sizes.RatingType);

            builder.HasOne(x => x.CommentTarget)
                .WithMany(x => x.Comments)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
