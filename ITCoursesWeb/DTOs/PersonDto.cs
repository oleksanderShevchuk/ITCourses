using ITCoursesWeb.Models;

namespace ITCoursesWeb.DTOs
{
    public class PersonDto
    {
        public string Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public ICollection<Course> Courses { get; set; } = new HashSet<Course>();
        public string PersonType { get; set; } = null!;
        public string AboutMe { get; set; } = null!;
        public ICollection<PromoCode> PromoCodes { get; set; } = new HashSet<PromoCode>();
    }
}
