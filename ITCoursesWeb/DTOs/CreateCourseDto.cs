namespace ITCoursesWeb.DTOs
{
    public class CreateCourseDto
    {
        public required string Name { get; set; }
        public string Description { get; set; } = null!;
        public string PathToImg { get; set; } = null!;
        public string TeacherName { get; set; } = null!;
    }
}
