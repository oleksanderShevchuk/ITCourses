using System.ComponentModel.DataAnnotations;

namespace ITCoursesWeb.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Course's name is required")]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; set; }
        public string PathToImg { get; set; }

        public int TeacherId { get; set; }
        public Person Teacher { get; set; }
        public ICollection<Person> Persons { get; set; } = new HashSet<Person>();
    }
}
