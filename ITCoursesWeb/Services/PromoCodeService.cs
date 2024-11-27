using System.Text;
using ITCoursesWeb.Data;
using ITCoursesWeb.DTOs;
using ITCoursesWeb.Interfaces;
using ITCoursesWeb.Models;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace ITCoursesWeb.Services
{
    public class PromoCodeService : IPromoCodeService
    {
        private readonly AppDbContext _context;
        public PromoCodeService(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<PromoCodeDto> GetAllByCourseId(string courseId)
        {
            var promoCodes = _context.PromoCodes.Where(p => p.CourseId == courseId);
            foreach (var promoCode in promoCodes)
            {
                yield return new PromoCodeDto
                {
                    Id = promoCode.Id,
                    Code = promoCode.Code,
                    CourseId = promoCode.CourseId,
                    DateTo = promoCode.DateTo,
                    IsUsed = promoCode.IsUsed,
                    Percent = promoCode.Percent,
                    PersonEmail = promoCode.Person?.Email!,
                };
            }
        }

        public async Task<PromoCodeDto> AddAsync(string courseId)
        {
            var promoCode = new PromoCode
            {
                Id = Guid.NewGuid().ToString(),
                Code = GeneratePromoCode(),
                CourseId = courseId,
                DateTo = DateTime.Now.AddDays(30),
                IsUsed = false,
                Percent = 5,
            };

            _context.PromoCodes.Add(promoCode);
            await _context.SaveChangesAsync();

            return new PromoCodeDto
            {
                Id = promoCode.Id,
                Code = promoCode.Code,
                CourseId = promoCode.CourseId,
                Percent = promoCode.Percent,
                DateTo = promoCode.DateTo,
                IsUsed= promoCode.IsUsed,
            };
        }

        public async Task<PromoCodeDto> EditAsync(string id, UpdatePromoCodeDto updatePromoCodeDto)
        {
            var promoCode = await _context.PromoCodes.FindAsync(id);
            if (promoCode == null)
                return null!;

            promoCode.IsUsed = Convert.ToBoolean(updatePromoCodeDto.IsUsed);
            promoCode.Percent = updatePromoCodeDto.Percent;
            promoCode.DateTo = updatePromoCodeDto.DateTo;

            var person = await _context.Persons.FirstOrDefaultAsync(p => p.Email == updatePromoCodeDto.PersonEmail);
            if (person != null)
            {
                promoCode.Person = person;
                promoCode.PersonId = person.Id;
                person.PromoCodes.Add(promoCode);
            }

            await _context.SaveChangesAsync();

            return new PromoCodeDto
            {
                Id = promoCode.Id,
                Code = promoCode.Code,
                CourseId = promoCode.CourseId!,
                Percent = promoCode.Percent,
                DateTo = promoCode.DateTo,
                IsUsed = promoCode.IsUsed,
                PersonEmail = person?.Email!,
            };
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var promoCode = await _context.PromoCodes.FindAsync(id);
            if (promoCode == null)
                return false;

            _context.PromoCodes.Remove(promoCode);
            await _context.SaveChangesAsync();

            return true;
        }

        private string GeneratePromoCode(int length = 15)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder promoCode = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                promoCode.Append(chars[random.Next(chars.Length)]);
            }

            return promoCode.ToString();
        }
    }
}
