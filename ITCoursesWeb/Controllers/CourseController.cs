using ITCoursesWeb.DTOs;
using ITCoursesWeb.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ITCoursesWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse([FromBody] CreateCourseDto createCourseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _courseService.AddAsync(createCourseDto);
            return CreatedAtAction(nameof(GetCourseById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCourse(int id, [FromBody] UpdateCourseDto updateCourseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _courseService.EditAsync(id, updateCourseDto);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var success = await _courseService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _courseService.GetAllAsync();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var course = await _courseService.GetByIdAsync(id);
            if (course == null)
                return NotFound();

            return Ok(course);
        }
    }
}
