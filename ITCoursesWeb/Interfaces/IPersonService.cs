
using ITCoursesWeb.DTOs;
using ITCoursesWeb.Models;

namespace ITCoursesWeb.Interfaces
{
    public interface IPersonService
    {
        Task<PersonDto> GetByEmailAsync(string email);
        Task<PersonDto> EditAsync(string id, UpdatePersonDto updatePersonDto);
        Task<bool> DeletePersonFromCourseAsync(string personId, string courseId);
        Task<List<ReportInformation>> GetReportInformationAsync();
        Task<List<PersonDto>> GetAllByCourseIdAsync(string courseId);
    }
}
