using ITCoursesWeb.Data;
using ITCoursesWeb.DTOs;
using ITCoursesWeb.Interfaces;

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
                    PersonId = promoCode.PersonId,
                    PersonName = promoCode.Person?.Name!,
                };
            }
        }
    }
}
