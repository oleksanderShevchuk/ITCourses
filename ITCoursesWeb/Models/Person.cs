namespace ITCoursesWeb.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public ICollection<Course> Courses { get; set; } = new HashSet<Course>();
        public PersonType PersonType { get; set; }
    }
    public enum PersonType
    {
        Stusent,
        Teacher
    }
}
