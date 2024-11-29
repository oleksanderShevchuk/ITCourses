using ITCoursesWeb.Data;
using ITCoursesWeb.DTOs;
using ITCoursesWeb.Interfaces;
using ITCoursesWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace ITCoursesWeb.Services
{
    public class CourseService : ICourseService
    {
        private readonly AppDbContext _context; 
        public CourseService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CourseDto> AddAsync(CreateCourseDto createCourseDto)
        {
            var teacher = await _context.Persons.FirstOrDefaultAsync(t => t.Email == createCourseDto.TeacherEmail);
            if (teacher == null) 
                return null!;

            var course = new Course
            {
                Id = Guid.NewGuid().ToString(), 
                Name = createCourseDto.Name,
                Description = createCourseDto.Description,
                ImgUrl = createCourseDto.ImgUrl,
                Teacher = teacher! ?? null,
                TeacherId = teacher!.Id ?? null,
                UpdatedAt = DateTime.UtcNow,
                Price = createCourseDto.Price,
            };
            teacher.Courses.Add(course);

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            var resultCourse = await _context.Courses.FirstOrDefaultAsync(t => t.Id == course.Id);
            return new CourseDto
            {
                Id = course.Id,
                Number = resultCourse?.Number,
                Name = course.Name,
                Description = course.Description,
                ImgUrl = course.ImgUrl,
                TeacherEmail = course.Teacher!.Email ?? null!,
                TeacherName = course.Teacher.Name ?? null!,
                Price = course.Price,
            };
        }

        public async Task<CourseDto> EditAsync(string id, UpdateCourseDto updateCourseDto)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return null!;

            course.Name = updateCourseDto.Name ?? course.Name;
            course.Description = updateCourseDto.Description ?? course.Description;
            course.ImgUrl = updateCourseDto.ImgUrl ?? course.ImgUrl;
            course.UpdatedAt = DateTime.UtcNow;
            course.Price = updateCourseDto.Price;

            var teacher = await _context.Persons.FirstOrDefaultAsync(p => p.Email == updateCourseDto.TeacherEmail);
            course.Teacher = teacher!;
            course.TeacherId = teacher?.Id!;

            await _context.SaveChangesAsync();

            return new CourseDto
            {
                Id = course!.Id,
                Number = course.Number,
                Name = course.Name,
                Description = course.Description,
                ImgUrl = course.ImgUrl,
                TeacherEmail = course?.Teacher?.Email! ?? null!,
                TeacherName = course?.Teacher?.Name! ?? null!,
                Price = course!.Price,
            };
        }

        public async Task<bool> DeleteAsync(string id)
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
                .OrderBy(c => c.Number)
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    Number = c.Number,
                    Name = c.Name,
                    Description = c.Description,
                    ImgUrl = c.ImgUrl!,
                    TeacherEmail = c.Teacher!.Email,
                    TeacherName = c.Teacher.Name,
                    Price = c.Price,
                })
                .ToListAsync();
        }

        public async Task<CourseDto> GetByIdAsync(string id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return null!;
            var teacher = await _context.Persons.FirstOrDefaultAsync(t => t.Id == course.TeacherId);

            return new CourseDto
            {
                Id = course.Id,
                Number = course.Number,
                Name = course.Name,
                Description = course.Description,
                ImgUrl = course.ImgUrl!,
                TeacherEmail = teacher?.Email ?? null!,
                TeacherName = teacher?.Name ?? null!,
                Price = course.Price,
            };
        }
    }
}
