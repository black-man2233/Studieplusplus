using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudiePlusPlus.Domain.Users;
using StudiePlusPlus.Domain.ValueObjects;

namespace StudiePlusPlus.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("newsequentialid()");

        builder.Property(x => x.Email)
            .HasConversion(
                v => v.Value,
                v => new Email(v))
            // .HasColumnName("email")
            .IsRequired();

        // builder.Property(x => x.FirstName).HasColumnName("first_name").HasMaxLength(100);
        // builder.Property(x => x.LastName).HasColumnName("last_name").HasMaxLength(100);
        // builder.Property(x => x.LoginId).HasColumnName("login_id");
    }
}