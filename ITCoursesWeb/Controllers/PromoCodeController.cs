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
    }
}
