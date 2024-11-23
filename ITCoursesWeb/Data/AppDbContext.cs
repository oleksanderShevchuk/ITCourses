using ITCoursesWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace ITCoursesWeb.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<PromoCode> PromoCodes { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonCourse> PersonCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One-to-Many: Course to Teacher
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Teacher)
                .WithMany()
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PersonCourse>()
               .HasKey(pc => new { pc.PersonId, pc.CourseId });

            // Many-to-Many: Course to Person
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Persons)
                .WithMany(p => p.Courses)
                .UsingEntity<Dictionary<string, object>>(
                    "CoursePerson",
                    j => j.HasOne<Person>().WithMany().HasForeignKey("PersonId"),
                    j => j.HasOne<Course>().WithMany().HasForeignKey("CourseId")
                );

            // One-to-Many: Person to PromoCodes
            modelBuilder.Entity<Person>()
                .HasMany(g => g.PromoCodes)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            // Person-PromoCode ForeignKey Setup
            modelBuilder.Entity<PromoCode>()
                .HasOne(g => g.Person)
                .WithMany()
                .HasForeignKey(g => g.PersonId)
                .OnDelete(DeleteBehavior.Restrict);

            // Enum conversion: PersonType to string
            modelBuilder.Entity<Person>()
                .Property(p => p.PersonType)
                .HasConversion<string>();

            // Enum conversion: Status in PersonCourse to string
            modelBuilder.Entity<PersonCourse>()
                .Property(pc => pc.Status)
                .HasConversion<string>(); 
        }
    }
}
