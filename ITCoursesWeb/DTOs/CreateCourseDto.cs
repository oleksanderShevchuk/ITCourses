﻿namespace ITCoursesWeb.DTOs
{
    public class CreateCourseDto
    {
        public required string Name { get; set; }
        public string Description { get; set; } = null!;
        public string ImgUrl { get; set; } = null!;
        public string TeacherEmail { get; set; } = null!;
        public int Price { get; set; }
    }
}
