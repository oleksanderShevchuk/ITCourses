using ITCoursesWeb.Data;
using ITCoursesWeb.DTOs;
using ITCoursesWeb.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITCoursesWeb.Services
{
    public class PersonService : IPersonService
    {
        private readonly AppDbContext _context;
        public PersonService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<PersonDto> GetByEmailAsync(string email)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(p => p.Email == email);
            return new PersonDto
            {
                Id = person!.Id,
                AboutMe = person.AboutMe!,
                Courses = person.Courses,
                Email = person.Email,
                Name = person.Name,
                PersonType = person.PersonType.ToString(),
                PromoCodes = person.PromoCodes,
            };
        }

        public async Task<PersonDto> EditAsync(string id, UpdatePersonDto updatePersonDto)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person == null)
                return null!;

            person.Name = updatePersonDto.Name;
            person.Email = updatePersonDto.Email;
            person.AboutMe = updatePersonDto.AboutMe;

            await _context.SaveChangesAsync();

            return new PersonDto
            {
                Id = person!.Id,
                Name = person.Name,
                Email = person.Email,
                AboutMe = person.AboutMe,
                Courses = person.Courses,
                PersonType = person.PersonType.ToString(),
                PromoCodes = person.PromoCodes,
            };
        }

        public async Task<bool> DeletePersonFromCourseAsync(string personId, string courseId)
        {
            var person = await _context.Persons.FindAsync(personId);
            var course = await _context.Courses.FindAsync(courseId);
            if (person == null || course == null)
                return false;

            if(person.Id == course!.TeacherId)
            {
                course.Teacher = null;
                course.TeacherId = null;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
