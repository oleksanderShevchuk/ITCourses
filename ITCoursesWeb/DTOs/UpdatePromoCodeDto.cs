using ITCoursesWeb.Models;

namespace ITCoursesWeb.DTOs
{
    public class UpdatePromoCodeDto
    {
        public string? PersonEmail { get; set; }

        public bool IsUsed { get; set; }
        public int Percent { get; set; } 
        public DateTime DateTo { get; set; }
    }
}
