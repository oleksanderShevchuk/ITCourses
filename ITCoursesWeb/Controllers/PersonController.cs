using ITCoursesWeb.Interfaces;
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
    }
}
