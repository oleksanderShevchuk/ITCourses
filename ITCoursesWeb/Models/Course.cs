using System.ComponentModel.DataAnnotations;

namespace ITCoursesWeb.Models
{
    public class Course
    {
        public string Id { get; set; }
        public string? Number { get; set; }
        [Required(ErrorMessage = "Course's name is required")]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public string? ImgUrl { get; set; }

        public string? TeacherId { get; set; } 
        public Person? Teacher { get; set; } 
        public ICollection<Person> Persons { get; set; } = new HashSet<Person>();
        public int Price { get; set; }  
    }
}
