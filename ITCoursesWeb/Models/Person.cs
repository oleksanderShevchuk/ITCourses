namespace ITCoursesWeb.Models
{
    public class Person
    {
        public string Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public ICollection<Course> Courses { get; set; } = new HashSet<Course>();
        public PersonType PersonType { get; set; }
        public string AboutMe { get; set; } = null!;
        public ICollection<PromoCode> PromoCodes { get; set; } = new HashSet<PromoCode>();
    }
    public enum PersonType
    {
        Stusent,
        Teacher
    }
}
