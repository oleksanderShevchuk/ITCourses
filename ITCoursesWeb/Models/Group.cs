namespace ITCoursesWeb.Models
{
    public class Group
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public ICollection<Person> Persons { get; set; } = new HashSet<Person>();
    }
}
