using ITCoursesWeb.Models;

namespace ITCoursesWeb.DTOs
{
    public class PersonDto
    {
        public string Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public ICollection<string> Courses { get; set; } = new List<string>();
        public string PersonType { get; set; } = null!;
        public string AboutMe { get; set; } = null!;
        public int CountPromoCodes { get; set; }
    }
}
