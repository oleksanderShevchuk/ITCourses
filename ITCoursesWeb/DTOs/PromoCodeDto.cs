using ITCoursesWeb.Models;

namespace ITCoursesWeb.DTOs
{
    public class PromoCodeDto
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string PersonId { get; set; }
        public string PersonName { get; set; }
        public string IsUsed { get; set; }
        public string Percent { get; set; } = null!;
        public DateTime DateTo { get; set; }
        public string CourseId { get; set; } = null!;
    }
}
