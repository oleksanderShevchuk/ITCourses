namespace ITCoursesWeb.DTOs
{
    public class CreateCourseDto
    {
        public required string Name { get; set; }
        public string Description { get; set; } = null!;
        public string PathToImg { get; set; } = null!;
        public int TeacherId { get; set; }
    }
}
