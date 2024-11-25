using ITCoursesWeb.Data;
using ITCoursesWeb.DTOs;
using ITCoursesWeb.Interfaces;
using ITCoursesWeb.Models;
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
                AboutMe = person.AboutMe,
                Courses = person.Courses,
                Email = person.Email,
                Name = person.Name,
                PersonType = person.PersonType.ToString(),
                PromoCodes = person.PromoCodes,
            };
        }
    }
}
