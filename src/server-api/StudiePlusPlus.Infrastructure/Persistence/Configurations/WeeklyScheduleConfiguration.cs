using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudiePlusPlus.Domain.Scheduling;

namespace StudiePlusPlus.Infrastructure.Persistence.Configurations;

public class WeeklyScheduleConfiguration : IEntityTypeConfiguration<WeeklySchedule>
{
    public void Configure(EntityTypeBuilder<WeeklySchedule> builder)
    {
        builder.ToTable("weekly_schedules");

        builder.HasKey(x => x.Id);
    }
}