using System.ComponentModel.DataAnnotations;

namespace ITCoursesWeb.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Course's name is required")]
        public string Name { get; set; }
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public string PathToImg { get; set; } = null!;

        public int TeacherId { get; set; }
        public Person Teacher { get; set; } = null!;
        public ICollection<Person> Persons { get; set; } = new HashSet<Person>();
    }
}
