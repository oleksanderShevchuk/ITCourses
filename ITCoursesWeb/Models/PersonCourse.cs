namespace ITCoursesWeb.Models
{
    public class PersonCourse
    {
        public string PersonId { get; set; }
        public Person Person { get; set; }
        public string CourseId { get; set; }
        public Course Course { get; set; }
        public Status Status { get; set; }
    }
    public enum Status
    {
        Planned,
        Ongoing,
        Completed,
    }
}
