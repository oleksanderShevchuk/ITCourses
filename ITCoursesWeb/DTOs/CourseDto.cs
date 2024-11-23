namespace ITCoursesWeb.DTOs
{
    public class CourseDto
    {
        public string Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImgUrl { get; set; } = null!;
        public string TeacherName { get; set; } = null!;
    }
}
