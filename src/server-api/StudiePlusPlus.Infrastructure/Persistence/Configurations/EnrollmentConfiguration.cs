using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudiePlusPlus.Domain.Students;

namespace StudiePlusPlus.Infrastructure.Persistence.Configurations;

public sealed class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        builder.ToTable("Enrollments");

        builder.HasKey(x => x.Id);
        // builder.Property(x => x.Id).HasColumnName("id");
        // builder.Property(x => x.StudentId).HasColumnName("student_id");
        // builder.Property(x => x.ClassId).HasColumnName("class_id");
        
        builder.HasIndex(x => new { x.StudentId, x.ClassId }).IsUnique(false);
    }
}