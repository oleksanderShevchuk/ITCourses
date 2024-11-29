namespace ITCoursesWeb.Models
{
    public class ReportInformation
    {
        public string PersonName { get; set; } = null!;
        public int TotalAmount { get; set; }
        public int TotalDiscountAmount { get; set; }
        public int AverageCost { get; set; }
        public int CountCourses { get; set; }
        public IEnumerable<ReportCourseInformation>? Courses { get; set; }
    }
}
