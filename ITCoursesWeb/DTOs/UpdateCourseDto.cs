namespace ITCoursesWeb.DTOs
{
    public class UpdateCourseDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImgUrl { get; set; } = null!;
        public string TeacherName { get; set; } = null!;
    }
}
