using Contact_zoo_at_home.Core.Entities.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.ConcreteMapping
{
    internal class StandartContractEntityConfiguration : IEntityTypeConfiguration<StandartContract>
    {
        public void Configure(EntityTypeBuilder<StandartContract> builder)
        {
            builder.HasBaseType<BaseContract>();
        }
    }
}
