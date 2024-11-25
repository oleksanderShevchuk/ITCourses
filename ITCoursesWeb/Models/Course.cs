using System.ComponentModel.DataAnnotations;

namespace ITCoursesWeb.Models
{
    public class Course
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Course's name is required")]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public string ImgUrl { get; set; } = null!;

        public string TeacherId { get; set; } = null!;
        public Person Teacher { get; set; } = null!;
        public ICollection<Person> Persons { get; set; } = new HashSet<Person>();
        public int Price { get; set; }  
    }
}
