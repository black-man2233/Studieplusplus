using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using StudiePlusPlus.Domain.Academics;
using StudiePlusPlus.Domain.Scheduling;
using StudiePlusPlus.Domain.Students;
using StudiePlusPlus.Domain.Teachers;
using StudiePlusPlus.Domain.Users;

namespace StudiePlusPlus.Infrastructure.Persistence;

public sealed class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();
    public DbSet<Teacher> Teachers => Set<Teacher>();
    public DbSet<Grade> Grades => Set<Grade>();
    public DbSet<ClassGroup> ClassGroups => Set<ClassGroup>();
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<WeeklySchedule> WeeklySchedules => Set<WeeklySchedule>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}