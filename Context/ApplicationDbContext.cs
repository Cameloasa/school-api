
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolApi.Models;

namespace SchoolApi.Context;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseInstance> CourseInstances { get; set; }
    public DbSet<Grade> Grades { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Student ↔ CourseInstance (many‑to‑many)
        modelBuilder.Entity<Student>()
            .HasMany(s => s.CourseInstances)
            .WithMany(ci => ci.Students);

        // Course ↔ CourseInstance (one‑to‑many)
        modelBuilder.Entity<Course>()
            .HasMany(c => c.Instances)
            .WithOne(ci => ci.Course)
            .HasForeignKey(ci => ci.CourseId);

        // Student ↔ Grade (one‑to‑many)
        modelBuilder.Entity<Student>()
            .HasMany(s => s.Grades)
            .WithOne(g => g.Student)
            .HasForeignKey(g => g.StudentId);
    }
}
