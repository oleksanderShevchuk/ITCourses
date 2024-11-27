using ITCoursesWeb.Models;

namespace ITCoursesWeb.DTOs
{
    public class UpdatePromoCodeDto
    {
        public string? PersonEmail { get; set; }

        public string? IsUsed { get; set; }
        public int Percent { get; set; } 
        public DateTime DateTo { get; set; }
    }
}
