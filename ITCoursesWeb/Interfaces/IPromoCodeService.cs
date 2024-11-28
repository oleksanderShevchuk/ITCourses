
using ITCoursesWeb.DTOs;

namespace ITCoursesWeb.Interfaces
{
    public interface IPromoCodeService
    {
        IEnumerable<PromoCodeDto> GetAllByCourseId(string courseId);
        Task<PromoCodeDto> AddAsync(string courseId);
        Task<PromoCodeDto> EditAsync(string id, UpdatePromoCodeDto updatePromoCodeDto);
        Task<bool> DeleteAsync(string id);
        void GeneratePromoCodes(int countPromoCodes, string courseId, DateTime dateTo, int discount);
    }
}
