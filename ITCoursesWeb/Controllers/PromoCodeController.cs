using ITCoursesWeb.DTOs;
using ITCoursesWeb.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ITCoursesWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromoCodeController : Controller
    {
        private readonly IPromoCodeService _promoCodeService;

        public PromoCodeController(IPromoCodeService personService)
        {
            _promoCodeService = personService;
        }

        [HttpGet("by-courseId/{courseId}")]
        public IActionResult GetAllByCourseId(string courseId)
        {
            var promoCodes = _promoCodeService.GetAllByCourseId(courseId);
            if (promoCodes == null)
                return NotFound();

            return Ok(promoCodes);
        }

        [HttpPost("{courseId}")]
        public async Task<IActionResult> AddPromoCode(string courseId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _promoCodeService.AddAsync(courseId);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCourse(string id, [FromBody] UpdatePromoCodeDto updatePromoCodeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _promoCodeService.EditAsync(id, updatePromoCodeDto);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(string id)
        {
            var success = await _promoCodeService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
