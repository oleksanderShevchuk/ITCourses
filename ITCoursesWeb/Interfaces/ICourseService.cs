using ITCoursesWeb.DTOs;

namespace ITCoursesWeb.Interfaces
{
    public interface ICourseService
    {
        Task<CourseDto> AddAsync(CreateCourseDto createCourseDto);
        Task<CourseDto> EditAsync(string id, UpdateCourseDto updateCourseDto);
        Task<bool> DeleteAsync(string id);
        Task<IEnumerable<CourseDto>> GetAllAsync();
        Task<CourseDto> GetByIdAsync(string id);
    }
}
