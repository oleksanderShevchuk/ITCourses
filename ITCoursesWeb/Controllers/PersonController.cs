using ITCoursesWeb.DTOs;
using ITCoursesWeb.Interfaces;
using ITCoursesWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace ITCoursesWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("by-email/{email}")]
        public async Task<IActionResult> GetPersonByEmail(string email)
        {
            var course = await _personService.GetByEmailAsync(email);
            if (course == null)
                return NotFound();
            
            return Ok(course);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCourse(string id, [FromBody] UpdatePersonDto updatePersonDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _personService.EditAsync(id, updatePersonDto);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{personId}/{courseId}")]
        public async Task<IActionResult> DeletePersonFromCourse(string personId, string courseId)
        {
            var success = await _personService.DeletePersonFromCourseAsync(personId, courseId);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpGet("GetReportInformation")]
        public async Task<IActionResult> GetReportInformationByPersonId()
        {
            var reportInformation = await _personService.GetReportInformationAsync();
            if (reportInformation == null)
                return NotFound();

            return Ok(reportInformation);
        }
        
        [HttpGet("get-all-by-course-id/{courseId}")]
        public async Task<IActionResult> GetAllByCourseId(string courseId)
        {
            var persons = await _personService.GetAllByCourseIdAsync(courseId);
            if (persons == null)
                return NotFound();

            return Ok(persons);
        }
    }
}
