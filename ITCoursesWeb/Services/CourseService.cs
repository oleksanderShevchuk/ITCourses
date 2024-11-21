using ITCoursesWeb.Data;
using ITCoursesWeb.DTOs;
using ITCoursesWeb.Interfaces;
using ITCoursesWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace ITCoursesWeb.Services
{
    public class CourseService : ICourseService
    {
        private readonly AppDbContext _context; public CourseService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CourseDto> AddAsync(CreateCourseDto createCourseDto)
        {
            var teacher = await _context.Persons.FirstOrDefaultAsync(t => t.Name == createCourseDto.TeacherName);
            var course = new Course
            {
                Name = createCourseDto.Name,
                Description = createCourseDto.Description,
                PathToImg = createCourseDto.PathToImg,
                Teacher = teacher!,
                TeacherId = teacher!.Id,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return new CourseDto
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                PathToImg = course.PathToImg,
                TeacherName = course.Teacher.Name ?? null!,
            };
        }

        public async Task<CourseDto> EditAsync(int id, UpdateCourseDto updateCourseDto)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return null!;

            course.Name = updateCourseDto.Name ?? course.Name;
            course.Description = updateCourseDto.Description ?? course.Description;
            course.PathToImg = updateCourseDto.PathToImg ?? course.PathToImg;
            course.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            var teacher = await _context.Persons.FirstOrDefaultAsync(t => t.Id == course.TeacherId);

            return new CourseDto
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                PathToImg = course.PathToImg,
                TeacherName = teacher?.Name ?? course?.Teacher?.Name!,
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return false;

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<CourseDto>> GetAllAsync()
        {
            return await _context.Courses
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    PathToImg = c.PathToImg,
                    TeacherName = c.Teacher.Name
                })
                .ToListAsync();
        }

        public async Task<CourseDto> GetByIdAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return null!;
            var teacher = await _context.Persons.FirstOrDefaultAsync(t => t.Id == course.TeacherId);

            return new CourseDto
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                PathToImg = course.PathToImg,
                TeacherName = teacher?.Name ?? "Unknown",
            };
        }
    }
}
