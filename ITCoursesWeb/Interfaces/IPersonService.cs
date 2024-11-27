
using ITCoursesWeb.DTOs;

namespace ITCoursesWeb.Interfaces
{
    public interface IPersonService
    {
        Task<PersonDto> GetByEmailAsync(string email);
        Task<PersonDto> EditAsync(string id, UpdatePersonDto updatePersonDto);
        Task<bool> DeletePersonFromCourseAsync(string personId, string courseId);
    }
}
