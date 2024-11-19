using ITCoursesWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace ITCoursesWeb.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One-to-Many
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Teacher)
                .WithMany()
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            // Many-to-Many
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Persons)
                .WithMany(p => p.Courses)
                .UsingEntity<Dictionary<string, object>>(
                    "CoursePerson",
                    j => j.HasOne<Person>().WithMany().HasForeignKey("PersonId"),
                    j => j.HasOne<Course>().WithMany().HasForeignKey("CourseId")
                );

            // One-to-Many
            modelBuilder.Entity<Group>()
                .HasMany(g => g.Persons)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Group>()
                .HasOne(g => g.Teacher)
                .WithMany()
                .HasForeignKey(g => g.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Person>()
                .Property(p => p.PersonType)
                .HasConversion<string>();
        }
    }
}
