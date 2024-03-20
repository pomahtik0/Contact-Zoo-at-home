using Contact_zoo_at_home.Core.Entities.Comments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.CommentsAndNotificationsMapping
{
    internal class BaseCommentEntityConfiguration : IEntityTypeConfiguration<BaseComment>
    {
        public const string TableName = "Comments";
        public void Configure(EntityTypeBuilder<BaseComment> builder)
        {
            builder.UseTptMappingStrategy().ToTable(TableName);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Date);

            builder.Property(x => x.Text)
                .HasMaxLength(Sizes.CommentMaxLength);

            builder.HasOne(x => x.Author)
                .WithMany(x => x.MyComments)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
