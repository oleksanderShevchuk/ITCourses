namespace ITCoursesWeb.Models
{
    public class ReportCourseInformation
    {
        public string CourseName { get; set; } = null!;
        public int Price { get; set; }
        public int PriceWithDiscount { get; set; }
        public string Status { get; set; } = null!;
        public int Discount { get; set; } 
    }
}
