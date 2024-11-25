
using ITCoursesWeb.DTOs;

namespace ITCoursesWeb.Interfaces
{
    public interface IPromoCodeService
    {
        IEnumerable<PromoCodeDto> GetAllByCourseId(string courseId);
    }
}
