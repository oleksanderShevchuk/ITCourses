namespace ITCoursesWeb.Models
{
    public class PersonCourse
    {
        public string PersonId { get; set; } = null!;
        public Person Person { get; set; } = null!;
        public string CourseId { get; set; } = null!;
        public Course Course { get; set; } = null!;
        public Status Status { get; set; }
    }
    public enum Status
    {
        Planned,
        Ongoing,
        Completed,
    }
}
