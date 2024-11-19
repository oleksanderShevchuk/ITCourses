using ITCoursesWeb.DTOs;

namespace ITCoursesWeb.Interfaces
{
    public interface ICourseService
    {
        Task<CourseDto> AddAsync(CreateCourseDto createCourseDto);
        Task<CourseDto> EditAsync(int id, UpdateCourseDto updateCourseDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<CourseDto>> GetAllAsync();
        Task<CourseDto> GetByIdAsync(int id);
    }
}
