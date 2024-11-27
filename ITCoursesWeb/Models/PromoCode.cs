namespace ITCoursesWeb.Models
{
    public class PromoCode
    {
        public string Id { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? PersonId { get; set; } 
        public Person? Person { get; set; } = null!;
        public bool IsUsed { get; set; }
        public int Percent { get; set; }
        public DateTime DateTo {  get; set; }
        public string? CourseId { get; set; }
    }
}
