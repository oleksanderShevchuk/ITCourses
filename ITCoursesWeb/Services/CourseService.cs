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
            if (teacher == null)
            {
                var email = $"{createCourseDto.TeacherName}@itcourse.com";
                teacher = new Person
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = createCourseDto.TeacherName,
                    Email = email,
                    PersonType = PersonType.Teacher,
                    AboutMe = $"I am {createCourseDto.TeacherName}\nMy Email: {email}\nI am a teacher the course: {createCourseDto.Name}"
                };
            }
            var course = new Course
            {
                Id = Guid.NewGuid().ToString(),  // ToDo: remove when do the implantation unique id
                Name = createCourseDto.Name,
                Description = createCourseDto.Description,
                ImgUrl = createCourseDto.ImgUrl,
                Teacher = teacher!,
                TeacherId = teacher!.Id,
                UpdatedAt = DateTime.UtcNow
            };
            teacher.Courses.Add(course);

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return new CourseDto
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                ImgUrl = course.ImgUrl,
                TeacherName = course.Teacher.Name ?? null!,
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
            if (course.TeacherId != null)
            {
                var teacher = await _context.Persons.FirstOrDefaultAsync(p => p.Id == course.TeacherId);
                teacher!.Name = updateCourseDto.TeacherName;
            }
            else {
                var email = $"{updateCourseDto.TeacherName}@itcourse.com";
                var teacher = new Person
                {
                    Id = new Guid().ToString(),
                    Name = updateCourseDto.TeacherName,
                    Email = email,
                    Courses = {  course },
                    PersonType = PersonType.Teacher,
                    AboutMe = $"I am {updateCourseDto.TeacherName}\nMy Email: {email}\nI am a teacher the course: {course.Name}"
                };
                course.Teacher = teacher;
                course.TeacherId = teacher.Id;
            }
            await _context.SaveChangesAsync();

            return new CourseDto
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                ImgUrl = course.ImgUrl,
                TeacherName = course?.Teacher?.Name!,
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
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    ImgUrl = c.ImgUrl,
                    TeacherName = c.Teacher.Name
                })
                .ToListAsync();

            //var courses = await _context.Courses.ToListAsync();

            //return await Task.WhenAll(courses.Select(async c => new CourseDto
            //{
            //    Name = c.Name,
            //    Description = c.Description,
            //    ImgUrl = c.ImgUrl,
            //    TeacherName = (await _context.Persons.FirstOrDefaultAsync(t => t.Id == c.TeacherId))?.Name!,
            //}));
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
                Name = course.Name,
                Description = course.Description,
                ImgUrl = course.ImgUrl,
                TeacherName = teacher?.Name ?? "Unknown",
            };
        }
    }
}
