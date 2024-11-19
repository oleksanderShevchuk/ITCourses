namespace ITCoursesWeb.DTOs
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PathToImg { get; set; } = null!;
        public int TeacherId { get; set; }
    }
}
