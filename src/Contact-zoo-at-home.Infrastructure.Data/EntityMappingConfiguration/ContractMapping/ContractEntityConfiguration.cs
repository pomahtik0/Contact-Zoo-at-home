using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Pets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.ConcreteMapping
{
    internal class ContractEntityConfiguration : IEntityTypeConfiguration<BaseContract>
    {
        public const string TableName = "Contracts";

        public const string TableContractsToPets = "PetsInContract";

        public void Configure(EntityTypeBuilder<BaseContract> builder)
        {
            builder.UseTptMappingStrategy().ToTable(TableName);

            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.ContractDate);

            builder.Property(x => x.ActivityType);

            builder.Property(x => x.StatusOfTheContract);

            builder.Property(x => x.ContractAdress)
                .HasMaxLength(Sizes.CommentMaxLength);

            builder.HasMany(x => x.PetsInContract)
                .WithMany()
                .UsingEntity(TableContractsToPets,
                    l => l.HasOne(typeof(Pet)).WithMany().OnDelete(DeleteBehavior.Restrict),
                    r => r.HasOne(typeof(BaseContract)).WithMany().OnDelete(DeleteBehavior.Cascade));

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Contracts)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Contractor)
                .WithMany(x => x.Contracts)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Representative)
                .WithMany(x => x.ContractsToRepresent)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
