namespace ITCoursesWeb.DTOs
{
    public class GeneratePromoCodesDto
    {
        public int CountPromoCodes { get; set; }
        public string CourseId { get; set; }
        public DateTime DateTo { get; set; }
        public int Discount { get; set; }
    }
}
