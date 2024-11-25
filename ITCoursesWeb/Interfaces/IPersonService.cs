
using ITCoursesWeb.DTOs;

namespace ITCoursesWeb.Interfaces
{
    public interface IPersonService
    {
        Task<PersonDto> GetByEmailAsync(string email);
    }
}
