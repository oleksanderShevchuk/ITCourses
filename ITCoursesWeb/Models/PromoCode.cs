namespace ITCoursesWeb.Models
{
    public class PromoCode
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string PersonId { get; set; }
        public Person Person { get; set; }
        public string IsUsed { get; set; }
        public string Percent { get; set; } = null!;
        public DateTime DateTo {  get; set; }
        public string CourseId { get; set; } = null!;
    }
}
