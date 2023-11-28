using AnimalHealth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalHealth.Persistence.Configurations;

public class PairPriceConfiguration : IEntityTypeConfiguration<PricePair>
{
    public void Configure(EntityTypeBuilder<PricePair> builder) =>
        builder.HasKey(pair => pair.Id);
}