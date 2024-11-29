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
            var personCourse = await _context.PersonCourses.FirstOrDefaultAsync(pc => pc.CourseId == courseId && pc.PersonId == personId);
            if (personCourse == null )
                return false;

            _context.PersonCourses.Remove(personCourse);
            await _context.SaveChangesAsync();

            var isDeleted = !await _context.PersonCourses
                .AnyAsync(pc => pc.CourseId == courseId && pc.PersonId == personId);

            return isDeleted;
        }
        public async Task<List<ReportInformation>> GetReportInformationAsync()
        {
            var persons = await _context.Persons.ToListAsync();
            if (persons == null)
                return null!;

            var reportInformations = new List<ReportInformation>();

            foreach (var person in persons)
            {
                var personCourses = _context.PersonCourses.Where(pc => pc.PersonId == person.Id).ToList();
                if (personCourses == null)
                    return null!;

                int totalAmount = 0, totalDiscountAmount = 0, averageCost = 0, countCourses = 0;
                var reportCourseInformation = new List<ReportCourseInformation>();

                foreach (var personCourse in personCourses)
                {
                    var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == personCourse.CourseId);
                    if (course == null)
                        continue;
                    totalAmount += course.Price;
                    var promoCode = await _context.PromoCodes.FirstOrDefaultAsync(p => p.PersonId == person.Id && p.CourseId == course.Id);

                    var priceForPerson = course.Price;
                    var promoCodePercent = 0;
                    if (promoCode != null)
                    {
                        promoCodePercent = promoCode.Percent;
                        priceForPerson = (int)(course.Price * (1 - (promoCodePercent / 100.0)));
                    }
                    totalDiscountAmount += course.Price - priceForPerson;
                    reportCourseInformation.Add(new ReportCourseInformation
                    {
                        CourseName = course.Name,
                        Discount = promoCodePercent,
                        Price = course.Price,
                        Status = personCourse.Status.ToString(),
                        PriceWithDiscount = priceForPerson
                    });
                }
                countCourses = personCourses.Count;
                if (countCourses != 0)
                    averageCost = (totalAmount - totalDiscountAmount) / countCourses;

                reportInformations.Add(new ReportInformation
                {
                    AverageCost = averageCost,
                    CountCourses = countCourses,
                    PersonName = person.Name,
                    TotalAmount = totalAmount,
                    TotalDiscountAmount = totalDiscountAmount,
                    Courses = reportCourseInformation,
                });
            }

            return reportInformations;
        }

        public async Task<List<PersonDto>> GetAllByCourseIdAsync(string courseId)
        {
            var personCourses = _context.PersonCourses.Where(p => p.CourseId == courseId);
            var persons = new List<PersonDto>();

            foreach (var personCourse in personCourses)
            {
                var person = await _context.Persons.FirstOrDefaultAsync(c => c.Id == personCourse.PersonId);
                if (person != null)
                    persons.Add(new PersonDto
                    {
                        Id = person!.Id,
                        Name = person.Name,
                        Email = person.Email,
                        AboutMe = person.AboutMe,
                        PersonType = person.PersonType.ToString(),
                        Courses = person.Courses,
                        PromoCodes = person.PromoCodes,
                    });
            }

            return persons;
        }
    }
}
