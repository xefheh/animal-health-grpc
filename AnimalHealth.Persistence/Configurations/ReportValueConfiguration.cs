using AnimalHealth.Domain.BasicReportEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalHealth.Persistence.Configurations
{
    public class ReportValueConfiguration : IEntityTypeConfiguration<ReportValue>
    {
        public void Configure(EntityTypeBuilder<ReportValue> builder)
            => builder.HasKey(x => x.Id);
    }
}
