using ITCoursesWeb.Models;

namespace ITCoursesWeb.DTOs
{
    public class PromoCodeDto
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string PersonEmail { get; set; }
        public bool IsUsed { get; set; }
        public int Percent { get; set; } 
        public DateTime DateTo { get; set; }
        public string CourseId { get; set; } = null!;
    }
}
